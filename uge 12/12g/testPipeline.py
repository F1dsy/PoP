import unittest
import pipeline


class Tests(unittest.TestCase):

    def test_AddConst(self):

        addOne = pipeline.AddConst(1)
        num = 43
        numPlusOne = addOne.apply(num)
        self.assertEqual(numPlusOne, 44)

    def test_Repeater(self):

        num = 43

        repeatFour = pipeline.Repeater(4)
        numList = repeatFour.apply(num)
        self.assertListEqual(numList, [43, 43, 43, 43])

        repeatNeg = pipeline.Repeater(-2)
        empty = repeatNeg.apply(num)
        self.assertListEqual(empty, [])

    def test_SumNum(self):
        summer = pipeline.SumNum()
        res = summer.apply([1, 2, 3, 4, 5, 6, 7, 8, 9, 10])
        self.assertEqual(res, 55)

    def test_ProductNum(self):
        summer = pipeline.ProductSum()
        res = summer.apply([1, 2, 3, 4, 5])
        self.assertEqual(res, 120)


if __name__ == "__main__":
    unittest.main()


# >>> summer = SumNum()
# >>> summer.apply([1,2,3,4,5,6,7,8,9,10])
# 55
# >>> proder = ProductNum()
# >>> proder.apply([1,2,3,4,5])
# 120
