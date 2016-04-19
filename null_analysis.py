users = open('user_rel_features.csv','r')
output = open('user_preprocessed.csv','w')

count_null = 0
country_list = ["de","at","ch","non_dach"]

for user in users:
    user_details = user.split(',')
    user_details[len(user_details)-1] = user_details[len(user_details)-1].split('\n')[0]


    for i in range(6):
        if user_details[i]=="NULL":
            if i==1:
                user_details[1] = "0"
            if i==4:
                user_details[4] = "all"
            if i==5:
                user_details[5] = "0"

    if user_details[4] != "all":
        for i in range(0,5):
            output.write(user_details[i] + ',')
        output.write('\n')

    else:
        for country in country_list:
            for i in range(0,4):
                output.write(user_details[i] + ',')
            output.write(country+",")
            output.write('\n')

users.close()
output.close()

inp = open('user_preprocessed.csv','r')
out = open('user_inp_cluster.csv','w')

for line in inp:
    out.write(line.split(",\n")[0] + '\n')

inp.close()
out.close()

