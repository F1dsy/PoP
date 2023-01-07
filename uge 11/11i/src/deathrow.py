import csv
import copy


def get_data() -> list[dict]:
    """get_data from tx_deathrow_full.csv file in same folder.

    returns list of dictionaries with 18 fields.
    """
    with open("tx_deathrow_full.csv") as csvfile:
        death_list: list[dict] = []
        death_reader = csv.DictReader(csvfile)

        for row in death_reader:
            death_list.append(row)
        return death_list


def to_metric(deathrow_data: list[dict]) -> list[dict]:
    """to_metric expects list of dictionaries
    with at least the fields 'Weight' and 'Height',
    else throws KeyError.

    returns list of dictionaries with metric units.
    """
    data = copy.deepcopy(deathrow_data)
    for row in data:
        if bool(row["Weight"]):
            row["Weight"] = int(row["Weight"]) / 2.2046
        if bool(row["Height"]):
            height: str = row['Height']
            splitted = height.split("'")
            last = splitted[1].rstrip("\"")
            row["Height"] = (int(splitted[0])*12 + int(last))*2.54
    return data


def county_statistics(deathrow_data):
    """county_statistics counts up all occurences of a county in the 'County' field. 
    If 'County' field is missing it throws KeyError.

    returns dictionary with county as key and number of occurences as value.
    """
    counties: dict[str, int] = {}
    for row in deathrow_data:
        county = row["County"]
        try:
            counties[county] = counties[county] + 1
        except:
            counties[county] = 1
    return counties


def native_statistics(deathrow_data):
    """native_statistics counts up all occurences of a county in the 'Native County' field. 
    If 'Native County' field is missing it throws KeyError.

    returns dictionary with county as key and number of occurences as value.
    """
    counties: dict[str, int] = {}
    for row in deathrow_data:
        county = row["Native County"]
        try:
            counties[county] = counties[county] + 1
        except:
            counties[county] = 1
    return counties


def last_words_search(deathrow_data, words: list[str]) -> list[tuple]:
    """last_words_search searches 'Last Statement' field from deathrow_data and matches with words list. 
    If 'Last Statement' field is missing it throws KeyError.

    returns list of tuples with (full name, age at execution, last statement), of the matches.
    """
    results = []
    for row in deathrow_data:
        last: str = row["Last Statement"]
        for word in words:
            if last.find(word) != -1:
                name = row["First Name"] + " " + row["Last Name"]
                ageAtExecution = row["Age at Execution"]
                results.append((name, ageAtExecution, last))
                break
    return results
