class Language {
  final int id;
  final String name;
  final String langCode;
  const Language (this.id,this.name,this.langCode);
}

const List<Language> getLanguages = <Language> [
  Language(0,'Türkçe','tr'),
  Language(1,'English','en')
];