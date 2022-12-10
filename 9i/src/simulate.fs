module Drone

type Position = (float * float)

type Drone(startPosition: Position, destinationPosition: Position, speed: float) =
    let mutable position: Position = startPosition
    let mutable destination: Position = destinationPosition
    let mutable speed: float = speed

    member this.Position = position
    member this.Destination = destination
    member this.Speed = speed

    /// <summary>Fly the drone one second in time and move it correspondingly</summary>
    /// <returns>unit</returns>
    member this.Fly() : unit =

        let sub (x1, y1) (x2, y2) = (x1 - x2, y1 - y2)
        let add (x1, y1) (x2, y2) = (x2 + x1, y2 + y1)

        let scale (s) (x, y) = (s * x, s * y)

        let len (x: float, y: float) = sqrt ((x ** 2) + (y ** 2))

        let vec = sub destination position
        let vlen = len vec

        if vlen < speed then
            position <- destination
        else
            position <- add position (scale (speed * (1.0 / vlen)) vec)

    /// <summary>Checks if drone has arrived at destination</summary>
    /// <returns>Returns true if at destination else false</returns>
    member this.AtDestination() : bool =
        if position = destination then
            true
        else
            false

type Airspace() =
    let mutable drones: Drone list = []

    member this.Drones = drones

    /// <summary>Calculates the euclidean distance between two drone positions</summary>
    /// <param name="drone1">First drone</param>
    /// <param name="drone2">Second drone</param>
    /// <returns>Returns euclidean distance between two drone positions</returns>
    member this.DroneDist (drone1: Drone) (drone2: Drone) =
        let sub (x1, y1) (x2, y2) = (x2 - x1, y2 - y1)

        let len (x: float, y: float) =
            sqrt ((float (x) ** 2) + (float (y) ** 2))

        len (sub drone2.Position drone1.Position)


    /// <summary>Adds new drone to airspace. If parameters are given it uses those else they are randomly generated</summary>
    /// <param name="startPosition">Optional start position</param>
    /// <param name="destination">Optional destination</param>
    /// <param name="speed">Optional speed</param>
    /// <returns>unit</returns>
    member this.AddDrone(?startPosition: Position, ?destination: Position, ?speed: float) =
        let posScalar = 5000.0
        let speedScalar = 500.0
        let rnd = System.Random()

        let _startPosition: Position =
            if startPosition.IsSome then
                (startPosition.Value)
            else
                (rnd.NextDouble() * posScalar, rnd.NextDouble() * posScalar)

        let _destination =
            if destination.IsSome then
                destination.Value
            else
                (rnd.NextDouble() * posScalar, rnd.NextDouble() * posScalar)

        let _speed =
            if speed.IsSome then
                speed.Value
            else
                rnd.NextDouble() * speedScalar

        drones <-
            Drone(_startPosition, _destination, _speed)
            :: drones

    /// <summary>Fly all drones in airspace on second</summary>
    /// <returns>unit</returns>
    member this.FlyDrones() =
        List.iter (fun (drone: Drone) -> drone.Fly()) drones

    /// <summary>Checks which drones in airspace have collided (less than 5 m from eachother)</summary>
    /// <returns>Returns list of collided drones</returns>
    member this.Collide() : Drone list =
        let mutable collided = []

        let atDestination = List.filter (fun (drone: Drone) -> drone.AtDestination()) drones
        let flying = List.filter (fun (drone: Drone) -> not (drone.AtDestination())) drones


        let checkDrones (drone1: Drone) (drone2: Drone) : bool =
            if (this.DroneDist drone1 drone2) < 500 then
                true
            else
                false

        for i = 0 to flying.Length - 1 do

            for j = i + 1 to flying.Length - 1 do
                if checkDrones flying[i] flying[j] then
                    collided <- collided @ [ flying[i]; flying[j] ]

        collided <- List.distinct collided

        drones <-
            atDestination
            @ (List.filter (fun (drone: Drone) -> not (List.exists (fun (d: Drone) -> d = drone) collided)) flying)

        collided

type Assert() =
    /// <summary>Tests two values with given comparison function</summary>
    /// <param name="str">Description</param>
    /// <param name="a">Expected value</param>
    /// <param name="b">Real value</param>
    /// <param name="f">Comparison function</param>
    /// <returns>unit</returns>
    static member test (str: string) (a: 'a) (b: 'b) (f: 'a -> 'b -> bool) : unit =
        if f a b then
            printfn "Pass: %s" str
        else
            printfn "Fail: %A" str
