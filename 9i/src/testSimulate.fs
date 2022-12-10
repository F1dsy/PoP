open Drone

let testDrone () =
    let drone = Drone((0, 500), (0, 1000), 500)
    Assert.test "Drone Position is: (0,500)" (0.0, 500.0) drone.Position (=)
    Assert.test "Drone Destination is: (0,1000)" (0.0, 1000.0) drone.Destination (=)
    Assert.test "Drone Speed is: 500" 500.0 drone.Speed (=)
    Assert.test "Drone is not at Destination" false (drone.AtDestination()) (=)
    Assert.test "Drone is flying" () (drone.Fly()) (=)
    Assert.test "Drone is at Destination" true (drone.AtDestination()) (=)


testDrone ()

let testAirspace () =
    let airspace = Airspace()
    let drone = Drone((0, 0), (500, 500), 50)
    Assert.test "Airspace is empty" [] airspace.Drones (=)
    airspace.AddDrone((0, 500), (0, 1000), 300)
    Assert.test "Airspace has 1 drone" 1 airspace.Drones.Length (=)
    Assert.test "Drone distance is 500" 500.0 (airspace.DroneDist airspace.Drones.Head drone) (=)
    airspace.AddDrone((0, 500), (0, 1000), 300)
    airspace.FlyDrones()
    Assert.test "Drone position is (0,800)" (0.0, 800.0) (airspace.Drones.Head.Position) (=)
    Assert.test "Number of collided drones is 2" 2 (airspace.Collide().Length) (=)

testAirspace ()


let simulate () =
    let airspace = Airspace()
    let mutable collided: Drone list = []

    for _ = 1 to 10 do
        airspace.AddDrone()

    let simOneSecond (airspace: Airspace) =
        airspace.FlyDrones()
        collided <- collided @ airspace.Collide()
        printfn "Collided: %200A" (List.map (fun (drone: Drone) -> drone.ToString()) collided)
        printfn "Flying:   %200A" (List.map (fun (drone: Drone) -> drone.ToString()) airspace.Drones)
        printfn ""

    for _ = 1 to 10 do
        simOneSecond airspace

simulate ()
