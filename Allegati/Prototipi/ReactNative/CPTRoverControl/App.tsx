import { StyleSheet, View } from "react-native";
import DirectionalCross from "./components/DirectionalCross";

const styles = StyleSheet.create({
  container: {
    flex: 1,
    backgroundColor: "#fff",
    justifyContent: "center",
  },
  controls: {
    flex: 1,
  },
  directionalCross: {
    paddingBottom: "4em",
  },
});

const App = () => {
  return (
    <View style={styles.container}>
      <View style={styles.controls}></View>
      <View style={styles.directionalCross}>
        <DirectionalCross />
      </View>
    </View>
  );
};

export default App;
