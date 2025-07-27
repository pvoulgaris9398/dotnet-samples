let vowels = set  ['a';'e';'i';'o';'u']

let convertToPigLatin (str: string) =
    let firstChar = str.[0]
    if vowels.Contains firstChar then
        str + "yay"
    else
        str.[1..] + string firstChar + "ay"

convertToPigLatin "Peter"
convertToPigLatin "Cecil"
convertToPigLatin "Apple"