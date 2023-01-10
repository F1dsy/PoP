import unittest
import pipeline


class Tests(unittest.TestCase):

    def test_AddConst(self):

        addOne = pipeline.AddConst(1)
        num = 43
        numPlusOne = addOne.apply(num)
        self.assertEqual(numPlusOne, 44)
        print(addOne.description())

    def test_Repeater(self):

        num = 43

        repeatFour = pipeline.Repeater(4)
        numList = repeatFour.apply(num)
        self.assertListEqual(numList, [43, 43, 43, 43])
        print(repeatFour.description())

        repeatNeg = pipeline.Repeater(-2)
        empty = repeatNeg.apply(num)
        self.assertListEqual(empty, [])

    def test_SumNum(self):
        summer = pipeline.SumNum()
        res = summer.apply([1, 2, 3, 4, 5, 6, 7, 8, 9, 10])
        self.assertEqual(res, 55)
        print(summer.description())

    def test_ProductNum(self):
        summer = pipeline.ProductSum()
        res = summer.apply([1, 2, 3, 4, 5])
        self.assertEqual(res, 120)
        print(summer.description())

    def test_Map(self):
        step = pipeline.AddConst(4)
        map = pipeline.Map(step)
        input = [1, 2, 3, 4]
        mapped = map.apply(input)
        self.assertListEqual(mapped, [5, 6, 7, 8])
        print(map.description())

    def test_Pipeline(self):
        pipesteps = [
            pipeline.AddConst(4),
            pipeline.Repeater(5),
            pipeline.SumNum()
        ]
        pipe = pipeline.Pipeline(pipesteps)
        res = pipe.apply(3)
        self.assertEqual(res, 35)
        print(pipe.description())

    def test_critter(self):
        cRead = pipeline.CsvReader()
        stats = pipeline.CritterStats()
        chart = pipeline.ShowAsciiBarChart()
        data = cRead.apply("critters.csv")
        stat = stats.apply(data)
        res = chart.apply(stat)
        print(res)

    def test_polynomial(self):
        inp = [0, 1, 2, 3, 4, 5]
        poly = pipeline.Polynomial([2, 0, 1])
        map = pipeline.Map(poly)
        res = map.apply(inp)
        print(res)


if __name__ == "__main__":
    unittest.main()


# >>> summer = SumNum()
# >>> summer.apply([1,2,3,4,5,6,7,8,9,10])
# 55
# >>> proder = ProductNum()
# >>> proder.apply([1,2,3,4,5])
# 120
