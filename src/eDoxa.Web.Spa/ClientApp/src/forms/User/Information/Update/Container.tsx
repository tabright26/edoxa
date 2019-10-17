import { connect } from "react-redux";
import { RootState } from "store/root/types";
import { loadUserInformations, updateUserInformations } from "store/root/user/informations/actions";
import Update from "./Update";
import moment from "moment";

const mapStateToProps = (state: RootState) => {
  const { data } = state.user.informations;
  const date = moment.unix(data.birthDate).toDate();
  return {
    initialValues: {
      ...data,
      birthDate: {
        year: date.getFullYear(),
        month: date.getMonth() + 1,
        day: date.getDate()
      }
    }
  };
};

const mapDispatchToProps = (dispatch: any) => {
  return {
    updateUserInformations: (data: any) => dispatch(updateUserInformations(data)).then(() => dispatch(loadUserInformations()))
  };
};

export default connect(
  mapStateToProps,
  mapDispatchToProps
)(Update);
