using UnityEngine;
using System.IO;
using UnityEditor;
	public abstract class GenericConfig<T> : ScriptableObject where T : GenericConfig<T>
	{
		private const string CONFIG_LOCATION = "Configs/";

		private static T s_instance;
		public static T Instance
		{
			get
			{
				if (s_instance == null)
				{
					Load();
					if (s_instance == null)
					{
						CreateConfig();
					}
				}
				return s_instance;
			}
			set
			{
				s_instance = value;
			}
		}
		private static void Load()
		{
			s_instance = Resources.Load<T>(typeof(T).ToString());
			if (s_instance == null)
			{
				s_instance = Resources.Load<T>(CONFIG_LOCATION + typeof(T).ToString());
			}
		}

		private static void CreateConfig()
		{
			string typeName = typeof(T).ToString();
			GenericConfig<T> asset = ScriptableObject.CreateInstance<T>();
			string savePath = "Assets/Resources/"+CONFIG_LOCATION;
			string assetName = string.Format("{0}.asset", typeName);
			ValidateDirectory(savePath);
			AssetDatabase.CreateAsset(asset, savePath + assetName);
			AssetDatabase.SaveAssets();
			s_instance = (T)asset;
		}

		private static void ValidateDirectory(string directory)
		{
			if (!Directory.Exists(directory))
			{
				Directory.CreateDirectory(directory);
			}
		}
	}