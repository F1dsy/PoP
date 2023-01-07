Name: Alexander Bang, dkg213

I have decided that the code should not cover non existent keys by itself, but should just return an "KeyError", 
if a field is not available. This makes the function simpler to write, but the user has to keep in mind, 
that all necessary fields are in the dictionary.

In my tests i have focused on covering most cases of failure, that could potentially happen, 
aswell as covering the simplest correct answers. I have not tested the missing Key Error.

To run test:
run "py testDeathrow.py" in command line, while in same folder.