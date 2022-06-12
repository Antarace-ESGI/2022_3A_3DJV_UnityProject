import bpy
import random

# Clear everything
bpy.ops.object.select_all(action='SELECT')
bpy.ops.object.delete(use_global=False, confirm=False)

# Generate bezier curve
bezier = []
y = 0
x = 0
length = int(random.random() * 10) + 10

for i in range(0, length):
    x += random.random()
    y += random.random() * 16 - 8

    segment = {
        "name": "",
        "location": (x * 8, y, random.random() * 2),
        "mouse": (random.random() * 100, random.random() * 100),
        "mouse_event": (0, 0),
        "pressure": 1,
        "size": 0,
        "pen_flip": False,
        "x_tilt": 0,
        "y_tilt": 0,
        "time": 0,
        "is_start": False
    }

    bezier.append(segment)

# Add bezier curve to scene
bpy.ops.curve.primitive_bezier_curve_add(enter_editmode=False, align='WORLD', location=(0, 0, 0), scale=(1, 1, 1))

curve = bpy.context.object

bpy.ops.object.editmode_toggle()
bpy.ops.curve.delete(type='VERT')

bpy.ops.curve.draw(error_threshold=-10,
                   fit_method='REFIT',
                   corner_angle=10,
                   use_cyclic=False,
                   stroke=bezier,
                   wait_for_input=False)

# Rainbow road

bpy.ops.mesh.primitive_cube_add(size=0.1, enter_editmode=False, align='WORLD', location=(0, 0, 0), scale=(1, 1, 1))
bpy.context.object.scale[1] = 10
bpy.ops.object.modifier_add(type='ARRAY')
bpy.context.object.modifiers["Array"].fit_type = 'FIT_CURVE'
bpy.context.object.modifiers["Array"].curve = curve
bpy.ops.object.modifier_add(type='CURVE')
bpy.context.object.modifiers["Curve"].object = curve
