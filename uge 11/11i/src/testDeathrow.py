import deathrow
import unittest


class Tests(unittest.TestCase):

    def test_get_data(self):
        first_data_row = deathrow.get_data()[0]
        expected = {'Execution': '553', 'Date of Birth': '1983-09-24', 'Date of Offence': '2004-11-21', 'Highest Education Level': '9', 'Last Name': 'Young', 'First Name': 'Christopher Anthony', 'TDCJ\nNumber': '999508', 'Age at Execution': '34', 'Date Received': '2006-03-31', 'Execution Date': '2018-07-17', 'Race': 'Black', 'County': 'Bexar',
                    'Eye Color': 'Brown', 'Weight': '216', 'Height': '6\' 1"', 'Native County': 'Bexar', 'Native State': 'Texas', 'Last Statement': "l want to make sure the Patel family knows I love them like they love me. Make sure the kids in the world know I'm being executed and those kids I've been mentoring keep this fight going. I'm good Warden."}

        self.assertDictEqual(first_data_row, expected)

    def testToMetric(self):
        imperial = [
            {
                "Weight": "200",
                "Height": '5\' 10"',
            }
        ]
        imperial_with_missing = []
        metric = deathrow.to_metric(imperial)
        metric_with_missing = deathrow.to_metric(imperial_with_missing)

        self.assertEqual(metric_with_missing, [])

        self.assertAlmostEqual(metric[0]["Height"], 177.8, 2)
        self.assertAlmostEqual(metric[0]["Weight"], 90.72, 2)

    def test_native_statistics(self):
        data = [
            {
                "Native County": "county1"
            },
            {
                "Native County": "county2"
            },
            {
                "Native County": "county1"
            },
            {
                "Native County": ""
            },
            {
                "Native County": None
            }
        ]
        expected = {
            "county1": 2,
            "county2": 1,
            "": 1,
            None: 1
        }
        native_data = deathrow.native_statistics(data)
        self.assertDictEqual({}, deathrow.native_statistics([]))

        self.assertDictEqual(native_data, expected)

    def test_county_statistics(self):
        data = [
            {
                "County": "county1"
            },
            {
                "County": "county2"
            },
            {
                "County": "county1"
            },
            {
                "County": ""
            },
            {
                "County": None
            }
        ]
        expected = {
            "county1": 2,
            "county2": 1,
            "": 1,
            None: 1
        }
        county_data = deathrow.county_statistics(data)

        self.assertDictEqual({}, deathrow.county_statistics([]))

        self.assertDictEqual(county_data, expected)

    def test_last_words_search(self):
        data = [{
            'Last Name': 'Young', 'First Name': 'Christopher Anthony',
            'Age at Execution': '34',
            'Last Statement': "l want to make sure the Patel family knows I love them like they love me."
        }]
        words = ["want", "sure"]
        search = deathrow.last_words_search(data, words)
        expected = ("Christopher Anthony Young", "34",
                    "l want to make sure the Patel family knows I love them like they love me.")

        self.assertListEqual(search, [expected])
        self.assertTupleEqual(search[0], expected)

        words_no_match = ["Lion", "Lucas"]
        search_no_match = deathrow.last_words_search(data, words_no_match)
        expected_no_match = []
        self.assertListEqual(search_no_match, expected_no_match)


if __name__ == '__main__':
    unittest.main()
