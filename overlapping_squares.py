import turtle

def draw_overlapping_squares(num_squares, size, overlap, color_list):
    # Set up the turtle
    turtle.speed(0)
    turtle.hideturtle()
    
    # Start drawing squares from the center
    turtle.penup()
    turtle.goto(-((size - overlap) * (num_squares - 1)) / 2, (size / 2))
    turtle.pendown()

    for i in range(num_squares):
        # Set color and start filling
        turtle.color(color_list[i % len(color_list)])
        turtle.begin_fill()
        
        # Draw square
        for _ in range(4):
            turtle.forward(size)
            turtle.right(90)
        
        turtle.end_fill()
        
        # Move to the next position
        turtle.penup()
        turtle.forward(size - overlap)
        turtle.pendown()

    # Finish up
    turtle.done()

# Parameters for the function
num_squares = 10
size = 50
overlap = 10
color_list = [
    "red", "blue", "green", "yellow", 
    "orange", "purple", "pink", "cyan", 
    "magenta", "black"
]

# Draw the squares
draw_overlapping_squares(num_squares, size, overlap, color_list)
