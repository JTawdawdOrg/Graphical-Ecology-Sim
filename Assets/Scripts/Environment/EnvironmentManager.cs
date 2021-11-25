using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEditor.AI;
using UnityEngine.AI;
using NavMeshBuilder = UnityEditor.AI.NavMeshBuilder;

public class EnvironmentManager : MonoBehaviour
{
	
	//Temporary, this should be taken in from a file
	private int[] terrainType;
	/*private int[] terrainType = new terrainType {
											0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,
											0,0,0,0,0,0,0,0,0,1,1,1,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,
											0,0,0,0,0,0,0,0,1,1,1,1,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,
											0,0,0,0,0,0,0,1,1,1,1,1,1,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,
											0,0,0,0,0,0,0,1,1,1,1,1,1,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,
											0,0,0,0,0,0,0,0,1,1,1,1,1,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,
											0,0,0,0,0,0,0,0,0,0,0,1,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,
											0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,
											0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,
											0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,
											0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,
											0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,1,
											0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,1,1,
											0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,1,1,1,
											1,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,1,1,1,1,
											1,1,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,1,1,1,1,
											1,1,1,1,1,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,1,1,1,1,
											1,1,1,1,1,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,1,1,1,1,
											1,1,1,1,1,1,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,1,1,1,1,
											1,1,1,1,1,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,1,1,1,1,
											1,1,1,1,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,1,1,1,1,1,
											1,1,1,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,1,1,1,1,1,
											0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,1,1,1,1,1,
											0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,1,1,1,1,1,
											0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,1,1,1,1,
											0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,1,1,
											0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,1,
											0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,
											0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,
											0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0
										  };
	*/
	public Tile[] tileArray;
	public Transform environmentTile;
	public Transform treePrefab;
	private int treeNumber;
	//public Vector2 environmentSize;
	private Vector2 environmentSize;
	
	void Start(){
		GetTerrainDataFromFile("/Materials/TerrainImage.png");
		//Debug.Log(Application.dataPath+"/Materials/TerrainImage.png");
		tileArray = new Tile[(int)(environmentSize.x*environmentSize.y)];
		GenerateTerrain();
		

	}
	
	int frame=0;
	void Update(){
		
		frame+=1;
	}
	
	public void GenerateTerrain(){
		//For each tile to place
		int tileNumber = 0;
		string terrainFolderName = "Terrain Folder";
		Transform terrainFolder = new GameObject(terrainFolderName).transform;
		terrainFolder.parent=transform;
		for(int x=0;x<environmentSize.x;x++){for(int y=0;y<environmentSize.y;y++){
			//Create tile
			Vector3 tilePosition = new Vector3(x, 0, y);
			Transform newTile = Instantiate(environmentTile, tilePosition, Quaternion.Euler(90,0,0)) as Transform;
			newTile.parent = terrainFolder;
			//Create Object
			tileArray[tileNumber] = new Tile(newTile, terrainType[tileNumber]);
			tileNumber+=1;
		}}
		//Modify the array by setting the adjacent tiles
		int environmentWidth = (int)environmentSize.x;
		Tile getTileAtIndex(int initialPosition, int index, int xColumn){//Function used to set adjacent tiles
			if(index>0&&index<tileArray.Length&&(xColumn==-1||xColumn==tileArray[index].getXPosition())){
				return tileArray[index];
			}
			else{
				return null;
			} 
		}
		for(int i=0;i<tileArray.Length;i++){
			//Set adjacent tiles
			Tile[,] localTiles = new Tile[3, 3];
			int correctColumn;
			
			localTiles[2,1]=getTileAtIndex(i, i-environmentWidth, -1);
			if (!(localTiles[2,1]==null)){correctColumn=localTiles[2,1].getXPosition();}
			else{correctColumn=-1;}
			localTiles[2,2]=getTileAtIndex(i, i-environmentWidth+1, correctColumn);
			localTiles[2,0]=getTileAtIndex(i, i-environmentWidth-1, correctColumn);
			
			localTiles[1,1]=getTileAtIndex(i, i, -1);
			if (!(localTiles[1,1]==null)){correctColumn=localTiles[1,1].getXPosition();}
			else{correctColumn=-1;}
			localTiles[1,2]=getTileAtIndex(i, i+1, correctColumn);
			localTiles[1,0]=getTileAtIndex(i, i-1, correctColumn);
			
			localTiles[0,1]=getTileAtIndex(i, i+environmentWidth, -1);
			if (!(localTiles[0,1]==null)){correctColumn=localTiles[0,1].getXPosition();}
			else{correctColumn=-1;}
			localTiles[0,2]=getTileAtIndex(i, i+environmentWidth+1, correctColumn);
			localTiles[0,0]=getTileAtIndex(i, i+environmentWidth-1, correctColumn);
			
			tileArray[i].setLocalTiles(localTiles);
		}
		//Set the appearence of the landscape
		foreach (Tile tile in tileArray){
			switch(tile.getTerrainType()){
				case 0:	//land
					if (tile.getSurrondingTilesOfType(1)>0){
						tile.setRoughColour(250f, 250f, 5f,20f);
						tile.setTerrainWetness(0.8f);
					}else{
						tile.setRoughColour(0f, 255f, 0f,20f);
						tile.setTerrainWetness(0.0f);
					}
					break;
				case 1:	//water
					if (tile.getSurrondingTilesOfType(0)>0){
						tile.setRoughColour(89f, 153f, 255f,20f);
						//tile.getTileTransform().position=new Vector3(tile.getTileTransform().position.x,-0.1f,tile.getTileTransform().position.z);
						tile.setTerrainWetness(1f);
					}else{
						tile.setRoughColour(20f, 110f, 255f,20f);
						//tile.getTileTransform().position=new Vector3(tile.getTileTransform().position.x,-0.2f,tile.getTileTransform().position.z);
						tile.setTerrainWetness(1.5f);
					}
					break;
				default:	//something's broken
					Debug.Log("Unrecognised terrainType "+tile.getTerrainType());
					break;
			}
		}
		foreach (Tile tile in tileArray){
			if (tile.getTerrainType()==0){
				if (!(tile.getTerrainWetness()==0.8f)){
					if(tile.getBroadSurrondingWetness()>0){
						tile.setRoughColour((0f+1000*tile.getBroadSurrondingWetness()), 255f, (0f+1000*tile.getBroadSurrondingWetness()),20f);
					}
				}
			}
		}
		//Add the trees
		int treesToPlace = treeNumber;
		while(treesToPlace>0){
			int xPosition = Random.Range(0, (int)environmentSize.x);
			int yPosition = Random.Range(0, (int)environmentSize.y);
			//if (getTile(xPosition,yPosition).getIsWalkable()==true){
			if (getTile(xPosition,yPosition).getBroadSurrondingWetness()<0.4f){
				Transform newTree = Instantiate(treePrefab, new Vector3(xPosition, 0, yPosition), Quaternion.identity) as Transform;
				newTree.parent = terrainFolder;
				Tile treeTile = getTile(xPosition,yPosition);
				treeTile.setIsWalkable(false);
				treeTile.setRoughColour(143f, 90f, 0f,10f);
				Tile adjacentTree;
				//The below blends nearby tiles with the brown tree colour
				try{adjacentTree=treeTile.getAdjacentTile(-1,0);if(adjacentTree.getIsWalkable()){adjacentTree.blendColour(143f, 90f, 0f,1,1);}}catch{}
				try{adjacentTree=treeTile.getAdjacentTile(1,0);if(adjacentTree.getIsWalkable()){adjacentTree.blendColour(143f, 90f, 0f,1,1);}}catch{}
				try{adjacentTree=treeTile.getAdjacentTile(0,-1);if(adjacentTree.getIsWalkable()){adjacentTree.blendColour(143f, 90f, 0f,1,1);}}catch{}
				try{adjacentTree=treeTile.getAdjacentTile(0,1);if(adjacentTree.getIsWalkable()){adjacentTree.blendColour(143f, 90f, 0f,1,1);}}catch{}
				treesToPlace-=1;
			}
		}
		
		/*
		//Do heights
		foreach(Tile tile in tileArray){
			Debug.Log(tile.getBroadSurrondingWetness());
			tile.getTileTransform().localScale = new Vector3(1,1,2-tile.getBroadSurrondingWetness());
		}
		*/
		
		//getTile(0,10).getTileTransform().GetComponent<Renderer>().material.SetColor("_Color", Color.red);
		
	}
	
	public Tile getTile(int xCoordinate, int yCoordinate){
		int position = xCoordinate*(int)environmentSize.y+yCoordinate;
		return tileArray[position];
	}
	public Tile getTile(Vector2 location){
		return getTile((int)location.x, (int)location.y);
	}
	
	public void GetTerrainDataFromFile(string path){
		//Texture2D image =  Resources.Load<Texture2D>("Materials/Terrain/TerrainImage");
		Texture2D image = Resources.Load("TerrainImage") as Texture2D;
		Debug.Log(image);
		int i=0;
		terrainType = new int[image.width*image.height];
		for (int x = 0; x < image.width; x++){
			for (int y = 0; y < image.height; y++){ 
				Color pixel = image.GetPixel(x, y);
				if ((pixel[0]+pixel[1]+pixel[2])/3>0.5){
					terrainType[i]=0;
					treeNumber+=1;
				}else{
					terrainType[i]=1;
				}
				i+=1;
			}
		}
		environmentSize=new Vector2(image.width, image.height);
		Debug.Log("Land Tiles: "+treeNumber);
		treeNumber=treeNumber/50;
	}
	
	
}
