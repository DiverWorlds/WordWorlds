###ブランチ
- main：すべての基礎となるブランチ。アジャイルの１サイクルが終わったときにdevelopからマージする。
  - develop：開発の中心となるブランチ。機能開発やバグ改善を行うときはこのブランチから以下のブランチを切って作業する。
    - feat/hoge_fuga：機能開発を行うときのブランチ。/(スラッシュ)以降は、その機能を簡潔に説明する。
    - fix/fuga_hoge：バグ修正を行うときのブランチ。/(スラッシュ)以降は、そのバグ内容を簡潔に説明する。
