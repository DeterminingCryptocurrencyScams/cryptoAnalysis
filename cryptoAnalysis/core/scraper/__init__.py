import scrapy


class UserScraper(object):
    BASEURL = "https://bitcointalk.org/index.php?action=profile;"

    def __init__(self):
        pass

    def start_requests(self):
        print('hi')
        for i in range(0, 100):
            yield scrapy.Request(url="${self.BASEURL}u=${i}", callback=self.parse)

    def parse(self, response):
        name = self.__get_name(response)
        posts = self.__get_posts(response)
        activity = self.__get_activity(response)
        merit = self.__get_merit(response)
        position = self.__get_position(response)
        date_registered = self.__get_date_registered(response)
        last_active = self.__get_last_active(response)
        gender = self.__get_gender(response)
        age = self.__get_age(response)
        location = self.__get_location(response)

    def __get_name(self, response):
        return response.xpath('//*[@id="bodyarea"]/table/tbody/tr/td/table/tbody/tr[2]/td[1]/table/tbody/tr[1]/td[2]').get()

    def __get_posts(self, response):
        return response.xpath('//*[@id="bodyarea"]/table/tbody/tr/td/table/tbody/tr[2]/td[1]/table/tbody/tr[2]/td[2]').get()

    def __get_activity(self, response):
        return response.xpath('//*[@id="bodyarea"]/table/tbody/tr/td/table/tbody/tr[2]/td[1]/table/tbody/tr[3]/td[2]').get()

    def __get_merit(self, response):
        return response.xpath('//*[@id="bodyarea"]/table/tbody/tr/td/table/tbody/tr[2]/td[1]/table/tbody/tr[4]/td[2]').get()

    def __get_position(self, response):
        return response.xpath('//*[@id="bodyarea"]/table/tbody/tr/td/table/tbody/tr[2]/td[1]/table/tbody/tr[5]/td[2]').get()

    def __get_date_registered(self, response):
        return response.xpath('//*[@id="bodyarea"]/table/tbody/tr/td/table/tbody/tr[2]/td[1]/table/tbody/tr[6]/td[2]').get()

    def __get_last_active(self, response):
        return response.xpath('//*[@id="bodyarea"]/table/tbody/tr/td/table/tbody/tr[2]/td[1]/table/tbody/tr[7]/td[2]').get()

    def __get_gender(self, response):
        return response.xpath('//*[@id="bodyarea"]/table/tbody/tr/td/table/tbody/tr[2]/td[1]/table/tbody/tr[18]/td[2]').get()

    def __get_age(self, response):
        return response.xpath('//*[@id="bodyarea"]/table/tbody/tr/td/table/tbody/tr[2]/td[1]/table/tbody/tr[19]/td[2]').get()

    def __get_location(self, response):
        return response.xpath('//*[@id="bodyarea"]/table/tbody/tr/td/table/tbody/tr[2]/td[1]/table/tbody/tr[20]/td[2]').get()
