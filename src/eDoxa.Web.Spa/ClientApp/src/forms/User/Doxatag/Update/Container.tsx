import { connect } from "react-redux";
import { RootState } from "store/types";
import Update from "./Update";

const mapStateToProps = (state: RootState) => {
  const { data } = state.root.user.doxatagHistory;
  const doxatag =
    data
      .slice()
      .sort((left: any, right: any) =>
        left.timestamp < right.timestamp ? 1 : -1
      )[0] || null;
  return {
    initialValues: doxatag
  };
};

export default connect(mapStateToProps)(Update);
