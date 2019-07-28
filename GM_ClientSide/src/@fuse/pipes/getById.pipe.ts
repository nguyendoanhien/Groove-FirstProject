import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
    name: 'getById',
    pure: false
})
export class GetByIdPipe implements PipeTransform {
    /**
     * Transform
     *
     * @param {any[]} value
     * @param {number} id
     * @param {string} property
     * @returns {any}
     */
    transform(value: any[], id: number, property: string): any {

        const foundItem = value.find(item => {
            if ( item.userId !== undefined )
            {
                return item.userId === id;
            }
            return false;
        });

        if (foundItem) {
            return foundItem[property];
        }
        if(property=='avatar')
            return 'assets/images/avatars/profile.jpg';
    }
}
