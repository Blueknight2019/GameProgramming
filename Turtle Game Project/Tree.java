import java.math.*;
import java.util.Scanner;
import java.awt.Color;

public class Tree
{

	public static void tree(Turtle t,double length,int n, int leftAngle, int rightAngle, Color color, boolean random)
	{
		if(n<=0)return;
		t.width(length/5);
		t.penColor(color);
		if(random)
		{
			Color yeet = new Color((int)(Math.random()*255), (int)(Math.random()*255), (int)(Math.random()*255));
			t.penColor(yeet);
		}
		t.forward(length);
		Turtle t2=t.clone();
		if(random)
		{
			Color yeet = new Color((int)(Math.random()*255), (int)(Math.random()*255), (int)(Math.random()*255));
			t.penColor(yeet);
		}
		t.left(leftAngle);
		t2.right(rightAngle);
		tree(t,length*.8,n-1, leftAngle, rightAngle, color, random);
		tree(t2,length*.7,n-1, leftAngle, rightAngle, color, random);
	}
	
	public static void main(String[] args)
	{
		Turtle bob=new Turtle();
		bob.up();
		bob.setPosition(0,-150,90);
		bob.down();
		bob.hide();
		bob.speed(0);
		Scanner scan = new Scanner(System.in);
		System.out.println("How long should the branches be?");
		double length = 0.0;
		length = scan.nextDouble();
		System.out.println("Angle of the left branches?");
		int left = 0;
		left = scan.nextInt();
		System.out.println("Angle of the right branches?");
		int right = 0;
		right = scan.nextInt();
		System.out.println("Number of recursions?");
		int recur = 0;
		recur = scan.nextInt();
		System.out.println("Color of branches? Tip: type 'random' for a rainbow tree.");
		String colour = "";
		colour = scan.next();
		boolean random = false;
		Color color = Turtle.getColor("black");
		if(colour.equals("rainbow"))
		{
			random = true;
		} 
		else 
		{
			color = Turtle.getColor(colour);
		}
		tree(bob, length, recur, left, right, color, random);
		bob.zoomFit();
	}	
}
