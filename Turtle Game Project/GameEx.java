import java.util.*;

public class GameEx
{
	public static void left(Turtle t)
	{
		t.left(7);
	}
	
	public static void right(Turtle t)
	{
		t.right(7);
	}
	
	public static void forward(Turtle t)
	{
		t.forward(10);
	}
	
	
	public static void main(String[] args)
	{
		ArrayList<Turtle> list=new ArrayList<Turtle>();
		Turtle bob=new Turtle();
		Turtle food=new Turtle(Math.random()*400-200,Math.random()*400-200);
		Turtle tracker=new Turtle(Math.random()*400-200,Math.random()*400-200);
		tracker.speed(0);
		tracker.fillColor("purple");
		food.up();
		food.speed(0);
		System.out.println(bob.contains(250,250));
		System.out.println(bob.contains(50,50));
		bob.speed(0);
		bob.onKey("left","left",true,true);
		bob.onKey("right","right",true,true);
		//bob.onKey("forward","space",true,true);
		while(true)
		{
			bob.home();
			bob.clear();
			tracker.clear();
			int speed=1;
			while(true)
			{
				for(int i=0;i<list.size();i++)
				{
					Turtle t=list.get(i);
					t.face(bob.getX(),bob.getY());
					t.forward(speed*Math.pow(.9,i+1));
					
				}
				tracker.face(bob.getX(),bob.getY());
				tracker.forward(speed*.8);
				if(bob.getX()>250 || bob.getX()<-250 ||bob.getY()>250 || bob.getY()<-250 )break;
				for(int i=0;i<speed;i++)
				{
					if(food.contains((int)Turtle.screenX(bob.getX()),(int)Turtle.screenY(-bob.getY()))) 
					{
						list.add(bob.clone());
						speed++;
						food.setPosition(Math.random()*400-200,Math.random()*400-200);
						System.out.println(speed);
						break;
					}
					bob.forward(1);
				}
				try
				{
					Thread.sleep(33);
				}
				catch(Exception e){}
			}
			System.out.println("womp womp whhh");
		}
	}	
}
