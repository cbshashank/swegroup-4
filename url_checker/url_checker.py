import httplib2 as httplib

base_urls = ["http://plants.usda.gov/gallery/pubs/",
             "http://plants.usda.gov/gallery/standard/",
             "http://plants.usda.gov/gallery/large/"]
plant_ids_file = open("plant_ids.txt")
sequence_numbers = ["001"]
sub_folders = ["pvp", "shp", "ahp", "avp", "avd", "lhp", "lv"]
formats = ["tif", "jpg"]
checkpoint = "brbr5"

connection = httplib.Http()


def list_pictures_urls():
    keep_skipping = True
    for plant_id in plant_ids_file:
        plant_id = plant_id.strip()
        if keep_skipping:
            if plant_id == checkpoint:
                keep_skipping = False
            else:
                continue
        working_url = get_a_working_url(plant_id)
        if working_url:
            print(str(plant_id) + " " + working_url)


def get_a_working_url(plant_id):
    for url in generate_urls(plant_id):
        if is_working(url):
            return url


def generate_urls(plant_id):
    for base in base_urls:
        for sequence_number in sequence_numbers:
            for sub_folder in sub_folders:
                for file_format in formats:
                    url = "_".join([plant_id, sequence_number, sub_folder])
                    yield base + url + "." + file_format


def is_working(url):
    response, content = connection.request(url, "GET")
    return response.status != 404

list_pictures_urls()

