# Texts Similarity Comparator

The python program takes in two texts by different authors and compare the writing style with a source text provided by the user.
The purpose is to find the text that is most likely to be written by the same author.
The proram checks the following fields:

1. The word lengths
2. The number of stems
3. The length of sentences
4. The frequency of long words

Here is an example of what the output might look like: <br>

scores for source1 : [-15.314905006836165, -7.56476341292621, -15.314905006836165, 0.0, -0.6931471805599453] <br>
scores for source2 : [-16.00805218739611, -12.652359748158592, -16.00805218739611, 0.0, 0] <br>
The given text is more likely to have come from source1
