export function CheckID(id) {

    // if (typeof str != "string") return false
    if (id.length != 9)
        return false;
    if (isNaN(id) || isNaN(parseFloat(id)))
        return false;
    let sum = 0;
    for (let i = 0; i < 9; i++) {
        let k = ((i % 2) + 1) * (parseInt(id[i]) - '0');
        if (k > 9)
            k -= 9;
        sum += k;
    }
    return sum % 10 === 0;


}
export function CheckName(name) {

    return /^[a-zA-Zא-ת\s]*$/.test(name);
}

