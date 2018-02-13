using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class ImageLoader : MonoBehaviour 
{
    // public Sprite[] spriteArray;
	// [Space(10)]
    public Texture2D[] textureArray;
	[HideInInspector]
	public bool finishedLoadingImages = false;

	private string[] files;
	private string pathPrefix; 
	
	public void GetDinoImages(string folderName)
	{
        pathPrefix = @"file:///";

		string dirPath = Path.Combine(Application.persistentDataPath, "Screenshots/" + folderName);

		if(Directory.Exists(dirPath))
		{
			files = Directory.GetFiles(dirPath, "*.jpg");

			StartCoroutine(LoadImages());
		}
		else
		{
			textureArray = new Texture2D[0];
			finishedLoadingImages = true;
		}
	}
	
	private IEnumerator LoadImages()
	{
		//load all images in default folder as textures and apply dynamically to plane game objects.
		//4 pictures per page
		textureArray = new Texture2D[files.Length];

		// if(textureArray.Length > 4)
		// 	spriteArray = new Sprite[4];

		// else if(textureArray.Length > 0 && textureArray.Length <= 4)
		// 	spriteArray = new Sprite[textureArray.Length];


		int i = 0;
		foreach(string filePath in files)
		{
			string imagePath = pathPrefix + filePath;

			WWW www = new WWW(imagePath);
			yield return www;
			Texture2D texTmp = new Texture2D(512, 512, TextureFormat.DXT1, false);  
			www.LoadImageIntoTexture(texTmp);
			
		
			textureArray[i] = texTmp;
			
			// if(i < spriteArray.Length)
			// {
			// 	spriteArray[i] = Sprite.Create(textureArray[i], new Rect(0, 0, textureArray[i].width, textureArray[i].height), new Vector2(0.5f, 0.5f));
			// }

			i++;

			if(i >= 4)
				break;
		}

		finishedLoadingImages = true;
	}
 }