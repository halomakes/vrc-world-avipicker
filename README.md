# Udon Avatar Picker
Paginated avatar picker to show off your and your friends' avatars!

## Usage
On the Avatar Picker component put your list of avatars in this format:
```json
{
  "author": [
    "id1234567890",
    "id0987654321"
  ]
}
```
![image](https://github.com/halomakes/vrc-world-avipicker/assets/5904472/ede1c346-a273-43ca-8b0e-4c045242cac5)

Place as many pedestals as you want throughout your scene and link them to the Pedestals array on that component. Page sizes will be determined by how many pedestals you pass in here.
![image](https://github.com/halomakes/vrc-world-avipicker/assets/5904472/c59a98af-1437-46d8-a66c-f2cefd8a0ee6)

The component does not currently generate toggle buttons dynamically (can't instantiate since Text does not derive from GameObject).  Create these yourself and place an AuthorButton script on them with the same author name you specified in your JSON.  The button component should call `OnButtonClicked` on your Udon Behavior.
![image](https://github.com/halomakes/vrc-world-avipicker/assets/5904472/2df75900-502b-4e87-aa16-a0313e6e0c0f)

Any Author or Pagination buttons must be under the Picker component as they look for a parent when communicating.
![image](https://github.com/halomakes/vrc-world-avipicker/assets/5904472/ccc69ebf-80d7-4568-9459-3adc172d0762)

Any elements or materials can be styled to your heart's content afterward. :D
