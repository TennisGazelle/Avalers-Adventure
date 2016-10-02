import random
generatorNdx = 0
while generatorNdx<100:
    if (generatorNdx%5) == 0:
        print(random.randint(25,75))
    else:
        print(random.randint(1,5))
    generatorNdx+=1
    
