import { Pipe, PipeTransform } from "@angular/core";

@Pipe({ name: "unknownContactFilter" })
export class UnknownContactFilterPipe implements PipeTransform {
    /**
     * Transform
     *
     * @param {any[]} mainArr
     * @param {string} searchText
     * @param {string} property
     * @returns {any}
     */
    transform(mainArr: any[], searchText: string, property: string): any {
        return mainArr.filter(itemObj => {
            return itemObj["displayName"].toLowerCase().includes(searchText.toLowerCase());
        });
    }
}