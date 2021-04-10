import { IPurchase } from '@/types/Purchase';
import js2excel from 'js2excel';

export default class ExcelService {
    private readonly dateFormat = 'dd.mm.yyyy';

    exportData(data: any, name: string) {
        js2excel.json2excel({
            data,
            name: name,
            formateDate: this.dateFormat
        });
    }
}