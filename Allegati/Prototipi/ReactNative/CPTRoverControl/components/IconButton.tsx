import { IconDefinition } from "@fortawesome/free-solid-svg-icons";
import { FontAwesomeIcon } from "@fortawesome/react-native-fontawesome";
import { IconButton as Button } from "@react-native-material/core";
import { StyleSheet } from "react-native";

const styles = StyleSheet.create({
  button: {
    backgroundColor: "#009aff",
  },
  icon: {
    color: "white",
  },
});

type Props = {
  icon: IconDefinition;
};

const IconButton = (props: Props) => {
  const { icon } = props;

  return <Button icon={<FontAwesomeIcon size={30} icon={icon} style={styles.icon} />} style={styles.button} />;
};

export default IconButton;
