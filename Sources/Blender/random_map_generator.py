import bpy
import random
import os

# Clear everything
bpy.ops.object.select_all(action='SELECT')
bpy.ops.object.delete(use_global=False, confirm=False)

# Generate bezier curve
bezier = []
y = 0#os.getenv("MIN_Y")
print(os.getenv("MIN_Y"))
x = 0#int(os.getenv("MIN_X"))
length = 10#int(os.getenv("TRACK_LENGTH"))

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

bpy.context.object.data.twist_mode = 'Z_UP'

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
bpy.ops.object.convert(target='MESH')

# Delete curve
bpy.ops.object.select_all(action='DESELECT')
bpy.data.objects[curve.name].select_set(True)
bpy.ops.object.delete()

#Export mesh
output_path = os.getenv("OUTPUT_PATH")

bpy.ops.object.select_by_type(type='MESH')
bpy.ops.object.origin_set(type="ORIGIN_GEOMETRY")
bpy.ops.export_scene.fbx(filepath=output_path)