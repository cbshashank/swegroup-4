file = open("data.txt")
file.readline()
for line in file:
    id = line.split('","')[0][1:]
    id = id.lower()
    print(id)