// Compiled with closure compiler with minor modifications

// Day 1 - 118 characters
function Day1(c){for(var b=0,d=-1,a=0;a<c.length;a++)"("===c[a]&&b++,")"===c[a]&&b--,-1===b&&0>d&&(d=a+1);return[b,d]}

// Day 2 - 235 characters
function Day2(b){b=b.split("\n");for(var d=0,e=0,f=0;f<b.length;f++)var a=b[f].split("x").sort(function(a,b){return a-b}),c=[a[0]*a[1],a[1]*a[2],a[2]*a[0]],d=d+(2*c[0]+2*c[1]+2*c[2]+c[0]),e=e+(2*a[0]+2*a[1]+a[0]*a[1]*a[2]);return[d,e]}

// Day 3 - 216 characters
function Day3(g){function b(b){for(var h=1,k={"0x0":1},d=0;d<b;d++)for(var e=0,f=0,c=d;c<g.length;c+=b){var a=g[c];"^"==a?e++:"v"==a?e--:">"==a?f++:"<"==a&&f--;a=e+"x"+f;k[a]||(h++,k[a]=1)}return h}return[b(1),b(2)]}

// Day 4 - 136 characters
function Day4(c){function a(a){for(var b=0,d="0".repeat(a);SparkMD5.hashBinary(c+ ++b).substring(0,a)!==d;);return b}return[a(5),a(6)]};
