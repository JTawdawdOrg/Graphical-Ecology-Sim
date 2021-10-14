using UnityEngine;

public class Tile {
	
	private Transform tile;
	private int terrainType;
	private float terrainWetness;
	private bool blocksMovement;
	private Tile[,] localTiles;
	private int xCoordinate;
	private int yCoordinate;
	private bool isWalkable;
	
	
	public Tile(Transform tile, int terrainType){
		this.tile = tile;
		this.terrainType = terrainType;
		xCoordinate = (int)this.tile.position.x;
		yCoordinate = (int)this.tile.position.y;
		if(terrainType==0){isWalkable=true;}//land
		else{isWalkable=false;}
	}
	
	public Tile getAdjacentTile(int xLoc, int yLoc){
		return localTiles[(-xLoc)+1,yLoc+1];//it's -xLoc because I screwed up the way they're selected
	}
	public int getSurrondingTilesOfType(int type){
		int numberMatching=0;
		for(int x=0;x<3;x++){for(int y=0;y<3;y++){
			if(!(localTiles[x,y]==null) && localTiles[x,y].getTerrainType()==type){
				numberMatching+=1;
			}
		}}
		return numberMatching;
	}
	public float getBroadSurrondingWetness(){
		float totalWetness=0;
		for(int x=0;x<3;x++){for(int y=0;y<3;y++){
			if (!(localTiles[x,y]==null) && !(x==1&&y==1)){
				totalWetness+=localTiles[x,y].getSurrondingWetness();
			}
		}}
		return (totalWetness/9);
	}
	public float getSurrondingWetness(){
		float totalWetness=0;
		for(int x=0;x<3;x++){for(int y=0;y<3;y++){
			if(!(localTiles[x,y]==null)){
				totalWetness+=localTiles[x,y].getTerrainWetness();
			}
		}}
		return (totalWetness/9);
	}
	
	
	//Colour changers
	public void setColour(float red, float green, float blue){
		getTileTransform().GetComponent<Renderer>().material.color = new Color(red/255f, green/255f, blue/255f);
	}
	public void blendColour(float red, float green, float blue,float ratioOriginal,float ratioNew){
		red = (red*ratioNew+ratioOriginal*getTileTransform().GetComponent<Renderer>().material.color[0]*255f)/(ratioNew+ratioOriginal);
		green = (green*ratioNew+ratioOriginal*getTileTransform().GetComponent<Renderer>().material.color[1]*255f)/(ratioNew+ratioOriginal);
		blue = (blue*ratioNew+ratioOriginal*getTileTransform().GetComponent<Renderer>().material.color[2]*255f)/(ratioNew+ratioOriginal);
		getTileTransform().GetComponent<Renderer>().material.color = new Color(red/255f, green/255f, blue/255f);
	}
	public void setRoughColour(float red, float green, float blue, float magnitude){
		getTileTransform().GetComponent<Renderer>().material.color = new Color((red+Random.Range(-(magnitude/2),magnitude/2))/255f, (green+Random.Range(-(magnitude/2),magnitude/2))/255f, (blue+Random.Range(-(magnitude/2),magnitude/2))/255f);
	}
	
	//Setters
	public void setBlocksMovement(bool blocksMovement){this.blocksMovement=blocksMovement;}
	public void setLocalTiles(Tile[,] localTiles){this.localTiles = localTiles;}
	public void setTerrainWetness(float terrainWetness){this.terrainWetness=terrainWetness;}
	public void setIsWalkable(bool isWalkable){this.isWalkable=isWalkable;}
	
	//Getters
	public Tile getAdjacentTile(Vector2 location){
		return getAdjacentTile((int)location.x, (int)location.y);
	}
	public Transform getTileTransform(){return tile;}
	public int getTerrainType(){return terrainType;}
	public Vector2 getPosition(){return new Vector2(xCoordinate,yCoordinate);}
	public int getXPosition(){return xCoordinate;}
	public int getYPosition(){return yCoordinate;}
	public float getTerrainWetness(){return terrainWetness;}
	public bool getIsWalkable(){return isWalkable;}
	
	
}
