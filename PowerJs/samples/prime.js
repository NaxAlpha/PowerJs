
out('Program to Display "N" Prime Numbers')

var n = inp('Enter N = ');
var x = 2;
var c = 1;

var isPrime = function(number){
	for(var i = 2;i<number;i++)
		if(number%i==0)
			return false;
	return true;
}

while (c<=n)
{
	if (isPrime(x))
	{
		out(c.toString() + 'th Prime is ' + x.toString());
		c++;
	}
	x++;
}

out('Press any key to continue...');
wait();