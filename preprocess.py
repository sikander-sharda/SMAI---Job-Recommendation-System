inp = open('user_inp_cluster.csv','r')
out = open('user_processed.csv','w')

career_level = [1,2,3,4,5,6]

for user in inp:
    user_details = user.split('\n')[0].split(',')
    if user_details[1] == "0":
        for i in career_level:
            out.write(user_details[0] + ',' + str(i) + ',')
            for j in range(2,len(user_details)-1):
                out.write(user_details[j] + ',')
            out.write(user_details[len(user_details)-1])
            out.write('\n')
    else:
        out.write(user)

inp.close()
out.close()

