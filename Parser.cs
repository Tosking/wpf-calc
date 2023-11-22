// See https://aka.ms/new-console-template for more information
public class Parser {

    private string ParseBrackets(string str){
        string result = "";
        for(int i = 0; i < str.Length; i++){
            string c = str[i].ToString();
            if(c == "("){
                result += ParseBrackets(str.Substring(i + 1));
            }
            if(c == ")"){
                return Parse(result).ToString();
            }
            result += c;
        }
        return "0";
    }
    public string Parse(string str)
    {
        List<string?> elements = new List<string?>();
        string temp = "";
        char[] priority = {'*', '/', '+', '-'};
        int size = 0;
        for(int i = 0; i < str.Length; i++){
            string c = str[i].ToString();

            if(int.TryParse(c, out int n) || c == ","){
                temp += c;
            }
            else {
                if(c == "("){
                    elements.Add(ParseBrackets(str.Substring(i+1)));
                    while(c != ")"){
                        if((str.Length - 1) <= i)
                            return "0";
                        i++;
                        c = str[i].ToString();
                    }
                    size++;
                }
                if(c != " "){
                    if(temp != ""){
                        elements.Add(temp);
                        size++;
                    }
                    elements.Add(c);
                    size++;
                    temp = "";
                }
            }
        }
        elements.Add(temp);
        size++;
        double result = 0;
        if(size <= 1){
            return elements[0] ?? "Error";
        }
        foreach(char op in priority){
            while(true){
                int i = elements.FindIndex(x => op.ToString() == x);
                if(i == -1)
                    break;
                double loper;
                string? sign = elements[i];
                double roper;
                if(!double.TryParse(elements[i - 1], out loper)){
                    return result.ToString();
                }
                if(!double.TryParse(elements[i + 1], out roper)){
                    return result.ToString();
                }
                if(double.TryParse(elements[i], out double n)){
                    return result.ToString();
                }
                if(i == 1 && (elements[1] == "+" || elements[1] == "-")){
                    result += loper;
                }
                switch(sign){
                    case "+":
                        result = loper + roper;
                        break;
                    case "-":
                        result = loper - roper;
                        break;
                    case "*":
                        result = loper * roper;
                        break;
                    case "/":
                        result = loper / roper;
                        break;
                }
                elements[i] = result.ToString();
                elements[i - 1] = null;
                elements[i + 1] = null;
                elements.RemoveAll(item => item == null);
            }
        }
        return result.ToString();
    }
}
