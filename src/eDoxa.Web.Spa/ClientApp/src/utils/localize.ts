import { localizeReducer, InitializePayload } from "react-localize-redux";
import { renderToStaticMarkup } from "react-dom/server";

export const reducer = localizeReducer;

export const initialize: InitializePayload = {
  languages: [{ name: "English", code: "en" }],
  options: {
    defaultLanguage: "en",
    renderToStaticMarkup
  }
};
