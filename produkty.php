<!DOCTYPE html>
<html lang="pl">

<head>
    <meta http-equiv="Content-Type" content="text/html" charset=utf-8" />
    <title>Lista produktów</title>
    <link href="https://fonts.googleapis.com/css?family=Oswald|Roboto+Condensed&amp;subset=latin-ext" rel="stylesheet">
    <link rel="stylesheet" href="styl.css">
</head>

<body>
    <nav>
        <a href="index.html">licznik-kalorii.pl</a>
        <a href="produkty.php">produkty</a>
        <a href="aplikacje.html">aplikacje</a>
        <a href="oprojekcie.html">o projekcie</a>

    </nav>
    <header>
        <h1>Lista produktów</h1>
    </header>

    <table>
        <tr>
            <th>ID</th>
            <th>Produkt w 100g</th>
            <th>Kalorie</th>
            <th>Białko</th>
            <th>Tłuszcze</th>
            <th>Węglowodany</th>
        </tr>
        <?php
$conn = mysqli_connect("licznik-kalorii.cba.pl", "czerwonysandal", "NiebieskiK2losz", "gymrun_project");
$conn->query("SET NAMES 'utf8'"); //ustawienie kodowania
// Check connection
if ($conn->connect_error) {
die("Connection failed: " . $conn->connect_error);
}
$sql = "SELECT IdProduktu, Nazwa, Kcal, Protein, Fats, Carbs FROM Produkty";
$result = $conn->query($sql);
if ($result->num_rows > 0) {
// output data of each row
while($row = $result->fetch_assoc()) {
echo "<tr><td>" . $row["IdProduktu"]. "</td><td>" . $row["Nazwa"] . "</td><td>"
. $row["Kcal"]. "</td><td>"
. $row["Protein"]. "</td><td>"
. $row["Fats"]. "</td>
<td>"
. $row["Carbs"]. "</td></tr>";
}
echo "</table>";
} else { echo "0 results"; }
$conn->close();
?>
    </table>





</body>

 

</html>
