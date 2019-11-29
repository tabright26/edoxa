import { connect } from "react-redux";
import { RootState } from "store/types";
import Update from "./Update";

const mapStateToProps = (state: RootState) => {
  const { data } = state.root.user.phone;
  return {
    initialValues: data
  };
};

export default connect(mapStateToProps)(Update);
