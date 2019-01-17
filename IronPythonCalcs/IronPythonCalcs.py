import collections
from collections import Counter

def compute_tf(text):
    tf_text = Counter(text)
    
    for i in tf_text:
        tf_text[i] = 0.5 + ( 0.5 * ( tf_text[i] / tf_text.most_common(1)[0][1] ))
        
    return tf_text

import math

def compute_idf(word, corpus):
    return math.log10(len(corpus)/sum([1.0 for i in corpus if word in i]))



