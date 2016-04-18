f = open('users.csv','r')
f.readline()
out = open('users_without_last.csv','w')
for line in f:
    line_details = line.split()
    line_details[len(line_details)-1] = line_details[len(line_details)-1].split('\n')[0]
    for i in range(11):
        out.write(line_details[i] + ',,,,')
    out.write('\n')
f.close()
out.close()

