import { Pipe, PipeTransform } from "@angular/core";
import { FacebookService } from "ngx-facebook";
import { AppHelperService } from "app/core/utilities/app-helper.service";
import { ApiMethod } from "ngx-facebook/dist/esm/providers/facebook";
import { environment } from "environments/environment";
import { HttpClient, HttpHeaders } from "@angular/common/http";

const httpOptions = {
    headers: new HttpHeaders({
        'Accept': "text/html, application/xhtml+xml, */*",
        'Content-Type': "application/json"
    }),
    responseType: "text" as "json"
};

@Pipe({ name: "extractUrl" })
export class ExtractUrlPipe implements PipeTransform {
    /**
     * Transform
     *
     * @param {any[]} mainArr
     * @param {string} searchText
     * @param {string} property
     * @returns {any}
     */
    constructor(
        public _appHelperService: AppHelperService,
        private fbk: FacebookService,
        private _httpClient: HttpClient) {


    }

    transform(message: string) {

        const res = this._appHelperService.ExtractUrl(message);

        let result: Promise<any[]>;
        if (res != null) {
            result = Promise.all(res.map(async (val): Promise<any> => {

                if (val.includes("cloudinary")) {
                    var obj = {
                        imgLink: val,
                        urlLink: null,
                        imgTitle: null
                    };
                    return obj;
                }

                var objOg = await this.getOg(val);
                if (objOg != null) {
                    var obj = {
                        imgLink: objOg.imageUrl,
                        urlLink: val,
                        imgTitle: objOg.titleUrl
                    };
                }
                return obj;
            })
            );


        }
        if (result !== undefined)
            result = result.then(data => data.filter(v => v));

        return result;

    }

    async getOg(urlPath: string) {
        var accessToken: string;
        await this.fbk.getLoginStatus().then(response => {

            if (response.status === "connected") {
                accessToken = response.authResponse.accessToken;
            } else {
                accessToken = environment.applicationFacebook.access_token;
            }
        });

        let obj: any;
        const apiMethod: ApiMethod = "post";

        await this.fbk.api(
            "/",
            apiMethod,
            { "access_token": accessToken, "scrape": "true", "id": urlPath }
        ).then(function (response) {

            obj = {
                imageUrl: response.image[0].url,
                titleUrl: response.title
            };

        }
        ).catch(err => {
            // this._httpClient.get(`https://besticon-demo.herokuapp.com/allicons.json?url=${urlPath}`, httpOptions).pipe(
            //     map((response: any) => {

            //         return response
            //     })

            //     // obj = null;
            // ).subscribe(response => {
            //     console.log(response);
            //     obj = {
            //         imageUrl: response.icons[0].url,
            //         titleUrl: response.url
            //     }
            // })
            var arr = urlPath.split("/");
            var result = arr[0] + "//" + arr[2];
            obj = {
                imageUrl: result + "/favicon.ico",
                titleUrl: null
            };

        });

        return obj;
    }
}