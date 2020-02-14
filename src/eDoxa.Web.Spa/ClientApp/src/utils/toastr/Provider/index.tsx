import React from "react";
import ReduxToastr from "react-redux-toastr";

export const ToastrProvider = () => (
  <ReduxToastr
    timeOut={7500}
    newestOnTop={false}
    preventDuplicates
    position="bottom-right"
    transitionIn="fadeIn"
    transitionOut="fadeOut"
    progressBar={false}
    closeOnToastrClick
  />
);
