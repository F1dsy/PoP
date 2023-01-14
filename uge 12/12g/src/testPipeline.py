import unittest
import pipeline
import operator


class Tests(unittest.TestCase):

    def test_AddConst(self):
        addFour = pipeline.AddConst(4)
        num = 43
        numPlusFour = addFour.apply(num)
        self.assertEqual(numPlusFour, 47)
        self.assertEqual(addFour.description(), "Add 4")

        addMinusFour = pipeline.AddConst(-4)
        num = 43
        numMinusFour = addMinusFour.apply(num)
        self.assertEqual(numMinusFour, 39)
        self.assertEqual(addMinusFour.description(), "Add -4")

    def test_Repeater(self):

        num = 43

        repeatFour = pipeline.Repeater(4)
        numList = repeatFour.apply(num)
        self.assertListEqual(numList, [43, 43, 43, 43])
        self.assertEqual(repeatFour.description(), "Repeat 4 times, as a list")

        repeatNeg = pipeline.Repeater(-2)
        empty = repeatNeg.apply(num)
        self.assertListEqual(empty, [])
        self.assertEqual(repeatNeg.description(), "Repeat 0 times, as a list")

    def test_GeneralSum(self):
        summer = pipeline.GeneralSum(200, operator.sub)
        res = summer.apply([100, 30, 20, 15, 3])
        self.assertEqual(res, 32)
        self.assertEqual(summer.description(),
                         "Do <built-in function sub> to each element, start from 200")

    def test_SumNum(self):
        summer = pipeline.SumNum()
        res = summer.apply([1, 2, 3, 4, 5, 6, 7, 8, 9, 10])
        self.assertEqual(res, 55)
        self.assertEqual(summer.description(), "Sum all elements")

    def test_ProductNum(self):
        summer = pipeline.ProductSum()
        res = summer.apply([1, 2, 3, 4, 5])
        self.assertEqual(res, 120)
        self.assertEqual(summer.description(), "Multiply all elements")

    def test_Map(self):
        step = pipeline.AddConst(4)
        map = pipeline.Map(step)
        input = [1, 2, 3, 4]
        mapped = map.apply(input)
        self.assertListEqual(mapped, [5, 6, 7, 8])
        self.assertEqual(map.description(), 'Add 4 to each element')

    def test_Pipeline(self):
        pipesteps = [
            pipeline.AddConst(4),
            pipeline.Repeater(5),
            pipeline.SumNum()
        ]
        pipe = pipeline.Pipeline(pipesteps)
        res = pipe.apply(3)
        self.assertEqual(res, 35)
        self.assertEqual(
            pipe.description(), "[Add 4 -> Repeat 5 times, as a list -> Sum all elements]")

        pipe.add_step(pipeline.AddConst(-10))
        newres = pipe.apply(3)
        self.assertEqual(newres, 25)
        self.assertEqual(
            pipe.description(), "[Add 4 -> Repeat 5 times, as a list -> Sum all elements -> Add -10]")

    def test_critter(self):
        cRead = pipeline.CsvReader()
        stats = pipeline.CritterStats()
        chart = pipeline.ShowAsciiBarChart()
        data = cRead.apply("critters.csv")
        stat = stats.apply(data)
        res = chart.apply(stat)
        print(res)

    def test_Exponent(self):
        expo = pipeline.Exponent(2)
        num = 3
        exponum = expo.apply(num)
        self.assertEqual(exponum, 9)
        self.assertEqual(expo.description(), "Do input^2")

        exponeg = pipeline.Exponent(-2)
        two = 2
        exponegnum = exponeg.apply(two)
        self.assertEqual(exponegnum, 0.25)
        self.assertEqual(exponeg.description(), "Do input^-2")

    def test_Logarithm(self):
        log = pipeline.Logarithm(10)
        num = 1000
        lognum = log.apply(num)
        self.assertAlmostEqual(lognum, 3)
        self.assertEqual(log.description(), "Find log with base 10")

    def test_polynomial(self):

        poly = pipeline.Polynomial([2, 0, 1])

        self.assertEqual(poly.apply(2), 9)
        self.assertEqual(poly.description(),
                         "Calculate polynomial with parameters [1, 0, 2]")

        inp = [0, 1, 2, 3, 4, 5]
        map = pipeline.Map(poly)
        res = map.apply(inp)
        self.assertListEqual(res, [1, 3, 9, 19, 33, 51])


if __name__ == "__main__":
    unittest.main()


# >>> summer = SumNum()
# >>> summer.apply([1,2,3,4,5,6,7,8,9,10])
# 55
# >>> proder = ProductNum()
# >>> proder.apply([1,2,3,4,5])
# 120
