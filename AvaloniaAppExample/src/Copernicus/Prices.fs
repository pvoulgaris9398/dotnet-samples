module Prices

type Currency = private Currency of string

type Price = 
   |Value of decimal
   | Security of string
