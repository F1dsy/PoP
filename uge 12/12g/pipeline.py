from abc import abstractmethod, ABC
import operator


class Step(ABC):

    @abstractmethod
    def apply(self, inp):
        return inp

    @abstractmethod
    def description(self) -> str:
        return self.__doc__.lower()


class AddConst(Step):

    def __init__(self, c: float) -> None:
        self.c = c
        super().__init__()

    def apply(self, inp):
        return self.c + inp

    def description(self) -> str:
        return super().description()


class Repeater(Step):

    def __init__(self, n: int) -> None:
        self.n = n
        super().__init__()

    def apply(self, inp):
        return self.n * [inp]

    def description(self) -> str:
        return super().description()


class GeneralSum(Step):

    def __init__(self, n, op: operator) -> None:
        self.n = n
        self.op = op
        super().__init__()

    def apply(self, inp: list):
        val = self.n
        for x in inp:
            val = self.op(val, x)
        return val

    def description(self) -> str:
        return super().description()


class SumNum(GeneralSum):
    def __init__(self) -> None:
        super().__init__(0, operator.add)


class ProductSum(GeneralSum):
    def __init__(self) -> None:
        super().__init__(1, operator.mul)
