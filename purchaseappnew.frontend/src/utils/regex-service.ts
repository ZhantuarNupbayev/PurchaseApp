export default class RegexService {
    private readonly dateRegex: RegExp = /^\s*(3[01]|[12][0-9]|0?[1-9])\.(1[012]|0?[1-9])\.((?:19|20)\d{2})\s*$/g;
    
    getRegexForDateSort(startDate: string, endDate: string): boolean {
        let startCheck = startDate.match(this.dateRegex);
        let endCheck = endDate.match(this.dateRegex);

        if (!startCheck || !endCheck) {
            return false;
        }
        return true;
    }


}