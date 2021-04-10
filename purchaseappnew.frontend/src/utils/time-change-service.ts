import { DateTime } from 'luxon';
import { format } from "date-fns";

export default class TimeChangeService {
    private readonly locale = "ru";
    private readonly defaultFormat = "yyyy-MM-dd";
    private readonly compareFormat = "dd.mm.yyyy";
    private readonly excelFormat = "dd.MM.yyyy_hh-mm-ss";
    
    formatDate(date: string): string {
        let dateTime = DateTime.fromISO(date);
        return dateTime.isValid ?
        dateTime.setLocale(this.locale)
        .toFormat(this.defaultFormat)
        : "";
    }

    compareTwoDates(startDate: string, endDate: string): boolean {
        let start = DateTime.fromFormat(startDate, this.compareFormat);
        let end = DateTime.fromFormat(endDate, this.compareFormat);

        if (start.startOf("day") > end.startOf("day")) {
            return false;
        }
        return true;
    }
 
    getCurrentDate(): DateTime {
        return DateTime.now();
    } 

    formatDateForExcelFile(): string {
        return DateTime.now().toFormat(this.excelFormat);
    }
}