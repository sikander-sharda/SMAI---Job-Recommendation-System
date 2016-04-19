import csv
import os
import sys
import bisect
from sets import Set


if __name__=="__main__":

	os.system('reset')
	#global train
	'''f = open(sys.argv[1], 'rb')
	user_id=[]
	c = 0
	try:
		data = csv.reader(f)
		for row in data:
			if c==0:
				c+=1
				continue
			user_id.append(int(row[0]))
			#print row[0]
	finally:
		f.close()
		'''
	one={}
	two={}
	three={}
	four={}
	with open(sys.argv[1], "r") as f2:
		#print user_id
		
		ids = {}
		rest = []
		c = 0
		for line in f2:
			if c==0:
				c+=1
				continue
			#print c
			c+=1
			temp = line.split(",")
			#print temp
			one[int(temp[1])]=one.get(int(temp[1]),Set([]))
			#if len(one[int(temp[1])])>0 and one[int(temp[1])][len(one[int(temp[1])])-1]!=temp[0]:
			one[int(temp[1])].add(temp[0])
			two[int(temp[2])]=two.get(int(temp[2]),Set([]))
			#if len(two[int(temp[2])])>0 and two[int(temp[2])][len(two[int(temp[2])])-1]!=temp[0]:
			two[int(temp[2])].add(temp[0])
			#two[int(temp[2])].append(temp[0])
			three[int(temp[3])]=three.get(int(temp[3]),Set([]))
			#if len(three[int(temp[3])])>0 and three[int(temp[3])][len(three[int(temp[3])])-1]!=temp[0]:
			three[int(temp[3])].add(temp[0])
			#three[int(temp[3])].append(temp[0])
			four[temp[4].split("\n")[0]]=four.get(temp[4].split("\n")[0],Set([]))
			#if len(four[temp[4]])>0 and four[temp[4]][len(four[temp[4]])-1]!=temp[0]:
			four[temp[4].split("\n")[0]].add(temp[0])
			#four[temp[4]].append(temp[0])
			#print temp[0]
			#id1 = int(temp[0])
			#ids.append(id1)
			ids[temp[0]] = ids.get(temp[0], [])
			ids[temp[0]].append(temp[1:])
			#rest.append(temp[1:])

	final = {}

	with open(sys.argv[2], "r") as f:
		out = open("output", "w")
		c=1
		string = ""
		for line in f:
			print c
			if c%1000==0:
				out.write(string)
				string=""
			c+=1
			temp = line.split(":")
			#out.write(temp[0])
			string+=temp[0]
			string+=":"
			#out.write(":")
			for item in temp[1:]:
				for item2 in range(0, len(item)):
					#out.write(item[item2].split("\n")[0])
					string+=item[item2].split("\n")[0]
					if item2!=len(item)-1:
						#out.write(" ")
						string +=" "
				#out.write(";")
				string += ";"
			#t = Set([])
			#out.write("{")
			string+="{"
			j = 0
			for i in range(1, len(temp)):
				temp2 = temp[i].split(" ")
				set1 = one[int(temp2[0])].intersection(two[int(temp2[1])])
				set2 = three[int(temp2[2])].intersection(four[temp2[3].split("\n")[0]])
				set3 = set1.intersection(set2)
				set2.clear()
				set1.clear()
				if j == 0:
					j = 1
					t = set3
					set3.clear()
				else:
					t = t.intersection(set3)
			for item in t:
				#out.write(item)
				string+=item
				#out.write(":")
				string+=":"
				for j in range(0, len(ids[item])):
					tempp = ids[item][j]
					for k in range(0, len(tempp)):
						#out.write(tempp[k].split("\n")[0])
						string += tempp[k].split("\n")[0]
						if k!=len(tempp)-1:
							#out.write(" ")
							string+=" "
					if j!=len(ids[item])-1:
						#out.write(",")
						string+=","
					else:
						#out.write(";")
						string+=";"
			#out.write("}\n")
			string+="}\n"
		out.write(string)
		out.close()
			#final[int(temp[0])] = t




		#print ids
	'''out2 = open("test_data", "w")
	c = 0
	for item in user_id:
		index = bisect.bisect(ids, item)
		out2.write(str(item))
		j = index-1
		while 1:
			if ids[j]!=item:
				break
				
			out2.write(":")
			for it in range(0,len(rest[j])):
				if it!=0:
					out2.write(" ")
				out2.write(rest[j][it].split('\n')[0])
			j-=1
		out2.write('\n')
	out2.close()'''

	
	#del ids[:]
	#del rest[:]


#Finding similar users:
'''	user_len = len(ids)
	with open("test_data", "r") as f3:
		cluster_dict = {}
		for line in f3:
			temp = line.split()
			#print temp
			
			c =0
			vec1 = []
			ind = [2, 3, 4, 5, 6, 10]
			#for i in range(0, len(ind)):
			#	vec1.append(temp[ind[i]])
			
			for i in range(0, user_len):
				if ids[i] == int(temp[0]):
					continue
				cnt = 0
				for j in range(0, len(ind)):
					if temp[ind[j]] == rest[i][ind[j]-1]:
						cnt+=1
				if cnt == len(ind):
					print "here"
					cluster_dict[int(temp[0])] = cluster_dict.get(int(temp[0]), [])
					cluster_dict[int(temp[0])].append([ids[i], rest[i][0]])'''

				
