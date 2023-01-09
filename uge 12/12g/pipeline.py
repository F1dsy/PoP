from abc import abstractmethod, ABC
import operator
import csv


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
        return f'Add {self.c}'


class Repeater(Step):

    def __init__(self, n: int) -> None:
        self.n = n
        super().__init__()

    def apply(self, inp):
        return self.n * [inp]

    def description(self) -> str:
        return f'Repeat {self.n} times, as a list'


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
        return f'Do {self.op} to each element, start from {self.n}'


class SumNum(GeneralSum):
    def __init__(self) -> None:
        super().__init__(0, operator.add)

    def description(self) -> str:
        return f'Sum all elements'


class ProductSum(GeneralSum):
    def __init__(self) -> None:
        super().__init__(1, operator.mul)

    def description(self) -> str:
        return f'Multiply all elements'


class Map(Step):
    def __init__(self, step: Step) -> None:
        self.step = step
        super().__init__()

    def apply(self, inp: list):
        res = []
        for x in inp:
            res.append(self.step.apply(x))
        return res

    def description(self) -> str:
        return f' {self.step.description()} to each element'


class Pipeline(Step):
    def __init__(self, steps: list[Step]) -> None:
        self.steps = steps
        super().__init__()

    def apply(self, inp: int):
        for x in self.steps:
            inp = x.apply(inp)
        return inp

    def add_step(self, step: Step):
        self.steps.append(step)

    def description(self) -> str:
        s = "[" + self.steps[0].description()
        for x in self.steps[1:]:
            s = s + " -> " + x.description()
        return s + "]"


class CsvReader(Step):
    def __init__(self) -> None:
        super().__init__()

    def apply(self, filestring: str):
        with open(filestring) as csvfile:
            result = []
            reader = csv.DictReader(csvfile)

            for row in reader:
                result.append(row)
            return result

    def description(self) -> str:
        return super().description()


class CritterStats(Step):
    def __init__(self) -> None:
        super().__init__()

    def apply(self, critter_data: list[dict]):
        stats: dict[str, int] = {}
        for row in critter_data:
            color = row["Colour"]
            try:
                stats[color] = stats[color] + 1
            except:
                stats[color] = 1
        return stats

    def description(self) -> str:
        return super().description()
