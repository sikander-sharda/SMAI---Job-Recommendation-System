f = open('users.csv','r')
f.readline()
out = open('user_rel_features.csv','w')
for line in f:
    line_details = line.split()
    line_details[len(line_details)-1] = line_details[len(line_details)-1].split('\n')[0]
    for i in range(11):
        if i!= 1:
            if i!=10:
                out.write(line_details[i] + ',')
            else:
                out.write(line_details[i])
    out.write('\n')
f.close()
out.close()

