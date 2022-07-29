import { faCaretDown, faCaretLeft, faCaretRight, faCaretUp } from "@fortawesome/free-solid-svg-icons";
import { StyleSheet, View } from "react-native";
import IconButton from "./IconButton";

const styles = StyleSheet.create({
  root: {
    alignItems: "center",
  },
  middle: {
    justifyContent: "space-between",
    flexDirection: "row",
    gap: "3em",
  },
});

const DirectionalCross = () => {
  return (
    <View style={styles.root}>
      <View>
        <IconButton icon={faCaretUp} />
      </View>
      <View style={styles.middle}>
        <IconButton icon={faCaretLeft} />
        <IconButton icon={faCaretRight} />
      </View>
      <View>
        <IconButton icon={faCaretDown} />
      </View>
    </View>
  );
};

export default DirectionalCross;
