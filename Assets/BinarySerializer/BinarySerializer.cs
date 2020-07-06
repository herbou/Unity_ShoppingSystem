using UnityEngine;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

/// <summary>
/// BinarySerializer is tool to save your game data in files as binary
/// so that no body can read it or modify it, 
/// this tool is generic.
/// 
/// Allowed types : int, float , strings, ...(all .Net types)  
/// But for unity types it accepts only 5 types: Vector2, Vector3, Vector4, Color, Quaternion.
/// 
/// Developed by Hamza Herbou
/// GITHUB : https://github.com/herbou
/// </summary>

public class BinarySerializer
{
	static string folderName = "GameData";

	static string persistentDataPath = Application.persistentDataPath;
	static SurrogateSelector surrogateSelector = GetSurrogateSelector ();

	/// <summary>
	/// Save data to disk.
	/// </summary>
	/// <param name="data">your dataClass instance.</param>
	/// <param name="filename">the file where you want to save data.</param>
	/// <returns></returns>
	public static void Save <T> (T data, string filename)
	{
		if (IsSerializable <T> ()) {
			if (!Directory.Exists (GetDirectoryPath ()))
				Directory.CreateDirectory (GetDirectoryPath ());

			BinaryFormatter formatter = new BinaryFormatter ();

			formatter.SurrogateSelector = surrogateSelector;

			FileStream file = File.Create (GetFilePath (filename));

			formatter.Serialize (file, data);

			file.Close ();
		}
	}

	/// <summary>
	/// Save data to disk.
	/// </summary>
	/// <param name="filename">the file where you saved data.</param>
	/// <returns></returns>
	public static T Load<T> (string filename)
	{
		T data = System.Activator.CreateInstance <T> ();

		if (IsSerializable <T> ()) {
			if (HasSaved (filename)) {
				
				BinaryFormatter formatter = new BinaryFormatter ();

				formatter.SurrogateSelector = surrogateSelector;

				FileStream file = File.Open (GetFilePath (filename), FileMode.Open);

				data = (T)formatter.Deserialize (file);

				file.Close ();
			}
		}

		return data;
	}

	static bool IsSerializable<T> ()
	{
		bool isSerializable = typeof(T).IsSerializable;
		if (!isSerializable) {
			string type = typeof(T).ToString ();
			Debug.LogError (
				"Class <b><color=white>" + type + "</color></b> is not marked as Serializable, "
				+ "make sure to add <b><color=white>[System.Serializable]</color></b> at the top of your " + type + " class."
			);
		}

		return isSerializable;
	}


	/// <summary>
	/// Check if data is saved.
	/// </summary>
	/// <param name="filename">the file where you saved data</param>
	/// <returns></returns>
	public static bool HasSaved (string filename)
	{
		return File.Exists (GetFilePath (filename));
	}

	static string GetDirectoryPath ()
	{
		return persistentDataPath + "/" + folderName;
	}

	static string GetFilePath (string filename)
	{
		return  GetDirectoryPath () + "/" + filename;
	}


	//Other non-serialized types /// SS: Serialization Surrogate
	//Vector2 , Vector3 , Vector4 , Color , Quaternion.

	static SurrogateSelector GetSurrogateSelector ()
	{
		SurrogateSelector surrogateSelector = new SurrogateSelector ();

		Vector2_SS v2_ss = new Vector2_SS ();
		Vector3_SS v3_ss = new Vector3_SS (); 
		Vector4_SS v4_ss = new Vector4_SS (); 
		Color_SS co_ss = new Color_SS ();
		Quaternion_SS qu_ss = new Quaternion_SS ();

		surrogateSelector.AddSurrogate (typeof(Vector2), new StreamingContext (StreamingContextStates.All), v2_ss);
		surrogateSelector.AddSurrogate (typeof(Vector3), new StreamingContext (StreamingContextStates.All), v3_ss);
		surrogateSelector.AddSurrogate (typeof(Vector4), new StreamingContext (StreamingContextStates.All), v4_ss);
		surrogateSelector.AddSurrogate (typeof(Color), new StreamingContext (StreamingContextStates.All), co_ss);
		surrogateSelector.AddSurrogate (typeof(Quaternion), new StreamingContext (StreamingContextStates.All), qu_ss);

		return surrogateSelector;
	}

	class Vector2_SS: ISerializationSurrogate
	{
		//Serialize Vector2
		public void GetObjectData (System.Object obj, SerializationInfo info, StreamingContext context)
		{
			Vector2 v2 = (Vector2)obj;
			info.AddValue ("x", v2.x);
			info.AddValue ("y", v2.y);
		}
		//Deserialize Vector2
		public System.Object SetObjectData (System.Object obj, SerializationInfo info, StreamingContext context, ISurrogateSelector selector)
		{
			Vector2 v2 = (Vector2)obj;

			v2.x = (float)info.GetValue ("x", typeof(float));
			v2.y = (float)info.GetValue ("y", typeof(float));

			obj = v2;
			return obj;
		}
	}

	class Vector3_SS: ISerializationSurrogate
	{
		//Serialize Vector3
		public void GetObjectData (System.Object obj, SerializationInfo info, StreamingContext context)
		{
			Vector3 v3 = (Vector3)obj;
			info.AddValue ("x", v3.x);
			info.AddValue ("y", v3.y);
			info.AddValue ("z", v3.z);
		}
		//Deserialize Vector3
		public System.Object SetObjectData (System.Object obj, SerializationInfo info, StreamingContext context, ISurrogateSelector selector)
		{
			Vector3 v3 = (Vector3)obj;

			v3.x = (float)info.GetValue ("x", typeof(float));
			v3.y = (float)info.GetValue ("y", typeof(float));
			v3.z = (float)info.GetValue ("z", typeof(float));

			obj = v3;
			return obj;
		}
	}

	class Vector4_SS: ISerializationSurrogate
	{
		//Serialize Vector4
		public void GetObjectData (System.Object obj, SerializationInfo info, StreamingContext context)
		{
			Vector4 v4 = (Vector4)obj;
			info.AddValue ("x", v4.x);
			info.AddValue ("y", v4.y);
			info.AddValue ("z", v4.z);
			info.AddValue ("w", v4.w);
		}
		//Deserialize Vector4
		public System.Object SetObjectData (System.Object obj, SerializationInfo info, StreamingContext context, ISurrogateSelector selector)
		{
			Vector4 v4 = (Vector4)obj;

			v4.x = (float)info.GetValue ("x", typeof(float));
			v4.y = (float)info.GetValue ("y", typeof(float));
			v4.z = (float)info.GetValue ("z", typeof(float));
			v4.w = (float)info.GetValue ("w", typeof(float));

			obj = v4;
			return obj;
		}

	}

	class Color_SS: ISerializationSurrogate
	{
		//Serialize Color
		public void GetObjectData (System.Object obj, SerializationInfo info, StreamingContext context)
		{
			Color color = (Color)obj;
			info.AddValue ("r", color.r);
			info.AddValue ("g", color.g);
			info.AddValue ("b", color.b);
			info.AddValue ("a", color.a);
		}
		//Deserialize Color
		public System.Object SetObjectData (System.Object obj, SerializationInfo info, StreamingContext context, ISurrogateSelector selector)
		{
			Color color = (Color)obj;

			color.r = (float)info.GetValue ("r", typeof(float));
			color.g = (float)info.GetValue ("g", typeof(float));
			color.b = (float)info.GetValue ("b", typeof(float));
			color.a = (float)info.GetValue ("a", typeof(float));

			obj = color;
			return obj;
		}
	}

	class Quaternion_SS: ISerializationSurrogate
	{
		//Serialize Quaternion
		public void GetObjectData (System.Object obj, SerializationInfo info, StreamingContext context)
		{
			Quaternion qua = (Quaternion)obj;
			info.AddValue ("x", qua.x);
			info.AddValue ("y", qua.y);
			info.AddValue ("z", qua.z);
			info.AddValue ("w", qua.w);
		}
		//Deserialize Quaternion
		public System.Object SetObjectData (System.Object obj, SerializationInfo info, StreamingContext context, ISurrogateSelector selector)
		{
			Quaternion qua = (Quaternion)obj;

			qua.x = (float)info.GetValue ("x", typeof(float));
			qua.y = (float)info.GetValue ("y", typeof(float));
			qua.z = (float)info.GetValue ("z", typeof(float));
			qua.w = (float)info.GetValue ("w", typeof(float));

			obj = qua;
			return obj;
		}
	}
}

