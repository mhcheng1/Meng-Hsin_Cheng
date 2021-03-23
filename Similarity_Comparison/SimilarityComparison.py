#!/usr/bin/env python3
# -*- coding: utf-8 -*-

"Compares two texts with a source text and use a similarity score to decide which of the writing style"
"is more similar to the source author"

import math

def clean_text(txt):
    """ clean all the punctuations and lower the cases of the string txt """
    
    txt= (txt.replace('.', '').replace(',', '').replace('?', '').replace('!', '').replace(';', '')
          .replace(':', '').replace('"', ''))
    txt = txt.lower()
    txt = txt.split()
    
    return txt



def stem(s):
    """ stem the word being given as s and return the root version of it"""
    if len(s) < 4:
        return s
    else:
        if s[-3:] == 'ies':
            s = s[0:-2]
        elif s[-3:] == 'ers':
            s = s[0:-3]
        elif s[-3:] == 'ing':
            s = s[0:-3]
        elif s[-2:] == 'er' and s[-3] != 'e':
            s = s[0:-2]
        elif s[-2:] == 'ly' and s[-3] != 'l':
            s = s[0:-2]
        elif s[-1] == 's' and s[-2] != 's':
            s = s[0:-1]
        elif s[-2:] == 'ty':
            s = s[0:-1] + 'i' 
        elif s[-2:] == 've':
            s = s[0:-1]
    return s
    
    
def compare_dictionaries(d1, d2):
    """ return the score of similarity of two given dictionaries d1 and d2 """
    score = 0
    total = len(d1)
    
    if total != 0:
        
        for n in d2:
            if n in d1:
                score += d2[n] * math.log(d1[n]/total) 
            else:
                score += d2[n] * math.log(0.5/total)

    return score



class TextModel:
    
    def __init__(self, model_name):
        self.name = model_name
        self.words = {}
        self.word_lengths = {}
        self.stems = {}
        self.sentence_lengths = {}
        
        ### dictionary for the words with length longer than 7
        self.long_words = {}
        
    
    def __repr__(self):
        """Return a string representation of the TextModel."""
        
        s = 'text model name: ' + self.name + '\n' + '  number of words: ' + str(len(self.words)) \
            + '\n' + '  number of word lengths: ' + str(len(self.word_lengths)) \
            + '\n' + '  number of stems: ' + str(len(self.stems)) \
            + '\n' + '  number of sentence lengths: ' + str(len(self.sentence_lengths)) \
            + '\n' + '  number of long words: ' + str(len(self.long_words))
        
        return s
    
    
    def add_string(self, s):
        """Analyzes the string txt and adds its pieces
        to all of the dictionaries in this text model.
        """
        sentences = s.split('.')
        for u in sentences:
            sentence = u.split()            
            if len(sentence) not in self.sentence_lengths:
                self.sentence_lengths[len(sentence)] = 1
            else:
                self.sentence_lengths[len(sentence)] += 1
            if 0 in self.sentence_lengths:
                del self.sentence_lengths[0]
       
        word_list = clean_text(s)
        
        for w in word_list:
            if w not in self.words:
                self.words[w] = 1
            else:
                self.words[w] += 1
                
    
        for x in word_list:
            if len(x) not in self.word_lengths:
                self.word_lengths[len(x)] = 1
            else:
                self.word_lengths[len(x)] += 1
                
        
        for z in range(len(word_list)):
            word_list[z] = stem(word_list[z])
            
        for y in word_list:
            if y not in self.stems:
                self.stems[y] = 1
            else:
                self.stems[y] += 1
                
                
        ### long_word
        for m in word_list:
            if len(m) > 7:
                if len(m) not in self.words:
                    self.long_words[len(m)] = 1
                else:
                    self.long_words[len(m)] += 1
        
        
    
    def add_file(self, filename):
        """ add the words in a file to the existing model """
        
        file = open(filename, 'r', encoding='utf8', errors='ignore')
        text = file.read()
        file.close()
        
        words = text.split()    
        self.add_string(words)
    
    
    
### Part II
    
    
    def save_model(self):
        """ create new files for each dictionaries and name them individually """
        
        filename = self.name + '_' + 'words'
        f = open(filename, 'w')
        f.write(str(self.words))           
        f.close()
        
        filename_1 = self.name + '_' + 'word_lengths'
        f = open(filename_1, 'w')
        f.write(str(self.word_lengths))           
        f.close()
        
        filename = self.name + '_' + 'stems'
        f = open(filename, 'w')
        f.write(str(self.stems))           
        f.close()
        
        filename = self.name + '_' + 'sentence_lengths'
        f = open(filename, 'w')
        f.write(str(self.sentence_lengths))           
        f.close()
        
        filename = self.name + '_' + 'long_words'
        f = open(filename, 'w')
        f.write(str(self.long_words))           
        f.close()
        
    
    
    def read_model(self):
        """ read into each of the stored dictionaries and assign it to the new attributes """
        
        f = open(self.name + '_' + 'words', 'r')    
        d_str = f.read()           
        f.close()
        
        d = dict(eval(d_str)) 
        self.words = d
        
        
        f = open(self.name + '_' + 'word_lengths', 'r')    
        d_str = f.read()           
        f.close()
        
        d = dict(eval(d_str)) 
        self.word_lengths = d
        
        
        f = open(self.name + '_' + 'stems', 'r')    
        d_str = f.read()           
        f.close()
        
        d = dict(eval(d_str)) 
        self.stems = d
        
        
        f = open(self.name + '_' + 'sentence_lengths', 'r')    
        d_str = f.read()           
        f.close()
        
        d = dict(eval(d_str)) 
        self.sentence_lengths = d
        
        f = open(self.name + '_' + 'long_words', 'r')    
        d_str = f.read()           
        f.close()
        
        d = dict(eval(d_str)) 
        self.long_words = d
        
        
        
    def similarity_scores(self, other):
        """ calculate each of the scores using the function of compare_dictionaries and return
        the result in a list """
        
        word_score = compare_dictionaries(other.words, self.words)
        word_lengths_score = compare_dictionaries(other.word_lengths, self.word_lengths)
        stems_score = compare_dictionaries(other.stems, self.stems)
        sentence_lengths_score = compare_dictionaries(other.sentence_lengths, self.sentence_lengths)
        long_words_score = compare_dictionaries(other.long_words, self.long_words)
    
        return [word_score, word_lengths_score, stems_score, sentence_lengths_score, long_words_score]
    
    
    def classify(self, source1, source2):
        """ calculate the scores of the two source and find out the closer source to the self source,
        print out the result """
        
        scores1 = self.similarity_scores(source1)
        scores2 = self.similarity_scores(source2)
    
        print('scores for', source1.name,':', scores1)
        print('scores for', source2.name,':', scores2)
        
        weighted_sum1 = 10*scores1[0] + 10*scores1[1] + 10*scores1[2] + 10*scores1[3] \
                        + 5*scores1[4]
        weighted_sum2 = 10*scores2[0] + 10*scores2[1] + 10*scores2[2] + 10*scores2[3] \
                        + 5*scores2[4]
        
        if weighted_sum1 > weighted_sum2:
            print(self.name, 'is more likely to have come from', source1.name)
        else:
            print(self.name, 'is more likely to have come from', source2.name)
        
    
    
def test():
    source1 = TextModel('source1')
    source1.add_string('It is interesting that she is interested.')

    source2 = TextModel('source2')
    source2.add_string('I am very, very excited about this!')

    mystery = TextModel('mystery')
    mystery.add_string('Is he interested? No, but I am.')
    mystery.classify(source1, source2)


    
def run_tests():
    source1 = TextModel('Enter Sample Text name')
    source1.add_file('Sample_text.txt')

    source2 = TextModel('Enter Sample Text 2 name')
    source2.add_file('Sample_text_2.txt')

    new1 = TextModel('Enter Text to compare author with')
    new1.add_file('test.txt')
    new1.classify(source1, source2)

    
    
    
    
    
    
    