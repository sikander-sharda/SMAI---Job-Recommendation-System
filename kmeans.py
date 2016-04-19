import numpy as np
from sklearn import clusters

user_dict = {}

X = np.array([])

#Index -> User_id

def get_user_data(filename="user_rel_features.csv"):
	fptr1 = open(filename, "r")
	i = 0
	data = fptr1.readline()
	for line in data:
		temp = line.split(",")
		user_dict[i] = int(temp[0]) #Get user id.
		
		for j in range(1, len(temp)):




if __name__=="__main__":
	get_user_data()