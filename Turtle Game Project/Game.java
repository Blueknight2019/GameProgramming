import java.util.*;
import java.io.*;
import javax.sound.sampled.*;

public class Game
{
	public static void main(String[] args)
	{
		Turtle.bgcolor("black");
		Turtle player = new Turtle();
		player.speed(0);
		player.up();
		player.setPosition(0, -432);
		player.shape("images/Ship.png");
		ArrayList<Turtle> aliens = new ArrayList<Turtle>();
		int counter = 0;
		for(int i = -5; i <= 5; i++)
		{
			for(int j = -2; j <= 2; j++)
			{
				Turtle alien = new Turtle();
				alien.speed(0);
				alien.up();
				if(j == 2) alien.shape("images/InvaderC1.png");
				if(j == 0 || j == 1) alien.shape("images/InvaderB1.png");
				if(j == -2 || j == -1) alien.shape("images/InvaderA1.png");
				alien.setPosition(i*160, j*50);
				aliens.add(alien);
			}
		}
		System.out.println("Click on the aliens to keep them at bay! (Fullscreen recommended.)");
		try
		{
			Thread.sleep(5000);
		}
		catch (Exception e) {}
		double start = System.nanoTime();
		boolean lose = false;
		while(true)
		{
			counter++;
			player.face(Turtle.canvasX(Turtle.mouseX()), Turtle.canvasY(Turtle.mouseY()));
			player.left(-90);
			if(Turtle.mouseButton1())
			{
				for(int i = 0; i < 55; i++)
				{
						Turtle alien = aliens.get(i);
						if((int)Turtle.canvasX(Turtle.mouseX()) > (int)alien.getX() -24 && (int)Turtle.canvasX(Turtle.mouseX()) < (int)alien.getX() +24 && (int)Turtle.canvasY(Turtle.mouseY()) > (int)alien.getY() -16 && (int)Turtle.canvasY(Turtle.mouseY()) < (int)alien.getY() +16)
						{
							alien.left(180);
							alien.forward(50);
							try
							{
								AudioInputStream audioInputStream = AudioSystem.getAudioInputStream(new File("sounds/InvaderHit.wav"));
								Clip clip = AudioSystem.getClip();
								clip.open(audioInputStream);
								clip.loop(1);
							}
							catch (Exception e) {}
						}
				}
			}
			for(int i = 0; i < 55; i++)
			{
				Turtle alien = aliens.get(i);
				if(counter % 2 == 0){
					if(i % 5 == 0 || i % 5 == 1) alien.shape("images/InvaderA1.png");
					if(i % 5 == 2 || i % 5 == 3) alien.shape("images/InvaderB1.png");
					if(i % 5 == 4) alien.shape("images/InvaderC1.png");
				}
				else if(counter % 2 == 1){
					if(i % 5 == 0 || i % 5 == 1) alien.shape("images/InvaderA2.png");
					if(i % 5 == 2 || i % 5 == 3) alien.shape("images/InvaderB2.png");
					if(i % 5 == 4) alien.shape("images/InvaderC2.png");
				}
				alien.face(player.getX(), player.getY());
				alien.forward(1);
				if((int)alien.getX() > (int)player.getX() - 30 && (int)alien.getX() < (int)player.getX() + 30 && (int)alien.getY() > (int)player.getY() -16 && (int)alien.getY() < (int)player.getY() +16)
				{
					lose = true;
				}
			}
			if(lose)
			{
				try
				{
					AudioInputStream audioInputStream = AudioSystem.getAudioInputStream(new File("sounds/ShipHit.wav"));
					Clip clip = AudioSystem.getClip();
					clip.open(audioInputStream);
					clip.loop(1);
				}
				catch (Exception e) {}
				player.exit();
				break;
			}
		}
		double finish = (System.nanoTime() - start) / 1e9;
		System.out.println("Congratulations! You survived the alien onslaught for " + finish + " seconds!");
	}
}
